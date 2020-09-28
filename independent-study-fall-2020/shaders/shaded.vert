#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

uniform vec4 PointLights [4];
uniform int PointLightsLength;
uniform vec4 DirectionLights [4];
uniform int DirectionLightsLength;

out vec2 v2f_uv;
out float v2f_shade;
out vec3 v2f_normal_world;

uniform mat4 ModelRotation;
uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    v2f_uv = in_uv;

    v2f_normal_world = (vec4(in_normal, 1f) * ModelRotation).xyz;

//    float shade = abs(dot(DirectionLights[0].xyz, v2f_normal_world));
//    v2f_shade = DirectionLightsLength;
    float diffuseSum = 0;
    for (int i = 0; i < DirectionLightsLength; i++){
        float diffuse = dot(DirectionLights[0].xyz, v2f_normal_world);
        diffuseSum += abs(diffuse);
    }
    float diffuseMean = diffuseSum / DirectionLightsLength;
    v2f_shade = diffuseMean;

//        float dotSums = 0;
//        int i;
//        for (i = 0; i < in_directionLights.length(); i++){
//            vec3 dir = in_directionLights[i].xyz;
//            float dotResult = dot(dir, in_normal);
//            dotSums += abs(dotResult);
//        }
//        float dotMean = dotSums/in_directionLights.length();
//
//        vec3 dir = in_directionLights[0].xyz;
//        float dotResult = dot(dir, in_normal);
//        v2f_shade = abs(dotResult);
    
    
}