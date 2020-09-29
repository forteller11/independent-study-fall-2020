﻿
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