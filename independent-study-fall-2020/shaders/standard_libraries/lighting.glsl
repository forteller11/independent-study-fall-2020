struct DirectionLight{
    vec3 Color;
    vec3 Direction;
};

struct PointLight{
    vec3 Color;
    vec3 Position;
    float Radius;
};

#define NR_LIGHTS 4
#define DIR_LIGHTS_DIST 100000
uniform DirectionLight DirectionLights [NR_LIGHTS];
uniform int DirectionLightsLength;

uniform PointLight PointLights [NR_LIGHTS];
uniform int PointLightsLength;

//https://gamedev.stackexchange.com/questions/56897/glsl-light-attenuation-color-and-intensity-formula
float attenuation_tweakable(float dist, float radius, float tweakA, float tweakB){
    float distRadius = dist/radius;
    return 1.0 / (1.0 + tweakA * distRadius + tweakB * distRadius*distRadius);
}

float attenuation(float dist, float radius){
    return attenuation_tweakable(dist, radius, 0.1, 7.5);
}

vec3 calculate_diffuse(vec3 worldNorm, vec3 worldPos){
    vec3 diffuseSum = vec3(0,0,0);
    
    for (int i = 0; i < DirectionLightsLength; i++){
        float product = dot(worldNorm, DirectionLights[i].Direction);
        float shade = clamp(product,0,1);
        vec3 diffuse = DirectionLights[i].Color * shade;
        diffuseSum += diffuse;
    }
    
    for (int i = 0; i < PointLightsLength; i++){
        vec3 lightToVert = PointLights[i].Position - worldPos;
        vec3 lightToVertDir = normalize(lightToVert);
    
        float product = dot(worldNorm, lightToVertDir);
        float shade = clamp(product,0,1);
        vec3 diffuse = PointLights[i].Color * shade;
        float attenuation = attenuation(distance(worldPos, PointLights[i].Position), PointLight[i].Radius);
        diffuseSum += diffuse * attenuation;
    }
    
    return diffuseSum;
}

vec3 calculate_specular(vec3 meshNormWorld, vec3 meshPosWorld, vec3 camPosWorld, float strength, float roughness){

    vec3 specSum = vec3(0,0,0);
    vec3 camToMesh = camPosWorld - meshPosWorld;
    vec3 camToMeshDir = normalize(camToMesh);
    
    for (int i = 0; i < PointLightsLength; i++){
        vec3 meshToLightDir = normalize(meshPosWorld - PointLights[i].Position);
        vec3 reflectedLightDir = reflect(meshToLightDir, meshNormWorld);
        float product = dot(reflectedLightDir, camToMeshDir);
        float shade = max(product, 0);
        float shadeCocentrated = pow(shade, roughness);
        float attenuation = attenuation(distance(worldPos, PointLights[i].Position), PointLight[i].Radius);
        
        specSum += shadeCocentrated * strength * attenuation * PointLights[i].Color;
    }
    
    for (int i = 0; i < PointLightsLength; i++){
    
        vec3 dirPosition = DirectionLights[i].Direction * DIR_LIGHTS_DIST;
        vec3 meshToLightDir = normalize(meshPosWorld - dirPosition);
        vec3 reflectedLightDir = reflect(meshToLightDir, meshNormWorld);
        float product = dot(reflectedLightDir, camToMeshDir);
        float shade = max(product, 0);
        float shadeCocentrated = pow(shade, roughness)  ;
        
        specSum += shadeCocentrated * strength * DirectionLights[i].Color;
    }
    
    

    
    return specSum;
}
