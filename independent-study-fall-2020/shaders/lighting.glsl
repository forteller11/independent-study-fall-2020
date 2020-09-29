
struct DirectionLight{
    vec3 Color;
    vec3 Direction;
};

struct PointLight{
    vec3 Color;
    vec3 Position;
};

#define NR_LIGHTS 4
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

vec3 calculate_specular(vec3 vertWorldNorm, vec3 vertPosWorld, vec3 camPosWorld){

    vec3 specSum = vec3(0,0,0);
    vec3 camToVert = vertPosWorld - camPosWorld;
    vec3 camToVertNorm = normalize(camToVert);
    
    for (int i = 0; i < PointLightsLength; i++){
        vec3 lightToVert = PointLights[i].Position - vertPosWorld;
        vec3 reflectedLight = reflect(lightToVert, vertWorldNorm);
        float product = dot(reflectedLight, camToVertNorm)*.5;
        float shade = clamp(product,0,2);
        float shadeCocentrated = pow(shade, 8) * .02;
        
        specSum += shadeCocentrated * PointLights[i].Color;
    }
    
    return specSum;
}