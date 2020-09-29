
struct DirectionLight{
    vec3 Color;
    vec3 Direction;
};

struct PointLight{
    vec3 Color;
    vec3 Position;
};

#define NR_LIGHTS 4
#define DIR_LIGHTS_DIST 100000
uniform DirectionLight DirectionLights [NR_LIGHTS];
uniform int DirectionLightsLength;

uniform PointLight PointLights [NR_LIGHTS];
uniform int PointLightsLength;

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
        diffuseSum += diffuse;
    }
    
    return diffuseSum;
}

vec3 calculate_specular(vec3 meshNormWorld, vec3 meshPosWorld, vec3 camPosWorld){

    vec3 specSum = vec3(0,0,0);
    vec3 camToMesh = camPosWorld - meshPosWorld;
    vec3 camToMeshDir = normalize(camToMesh);
    float specular_strength = 0.8f;
    
    for (int i = 0; i < PointLightsLength; i++){
        vec3 meshToLightDir = normalize(meshPosWorld - PointLights[i].Position);
        vec3 reflectedLightDir = reflect(meshToLightDir, meshNormWorld);
        float product = dot(reflectedLightDir, camToMeshDir); //.5 should not be necessary?
        float shade = max(product, 0);
        float shadeCocentrated = pow(shade, 32)  ;
        
        specSum += shadeCocentrated * specular_strength * PointLights[i].Color;
    }
    
    for (int i = 0; i < PointLightsLength; i++){
    
        vec3 dirPosition = DirectionLights[i].Direction * DIR_LIGHTS_DIST;
        vec3 meshToLightDir = normalize(meshPosWorld - dirPosition);
        vec3 reflectedLightDir = reflect(meshToLightDir, meshNormWorld);
        float product = dot(reflectedLightDir, camToMeshDir); //.5 should not be necessary?
        float shade = max(product, 0);
        float shadeCocentrated = pow(shade, 32)  ;
        
        specSum += shadeCocentrated * specular_strength * DirectionLights[i].Color;
    }
    

    
    return specSum;
}

