
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

vec3 calculate_diffuse(vec3 worldNormal){
    vec3 diffuseSum = vec3(0,0,0);
    
    for (int i = 0; i < DirectionLightsLength; i++){
        float product = dot(worldNormal, DirectionLights[i].Direction);
        float shade = clamp(product,0,1);
        vec3 diffuse = DirectionLights[i].Color * shade;
        diffuseSum += diffuse;
    }
    return diffuseSum;
}