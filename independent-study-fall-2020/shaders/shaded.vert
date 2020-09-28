#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

//in vec4 in_directionLights [4];
//in vec4 in_spotLights [4];

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

    v2f_shade = 1f;

    //    float dotSums = 0;
    //    int i;
    ////    for (i = 0; i < in_directionLights.length(); i++){
    ////        vec3 dir = in_directionLights[i].xyz;
    ////        float dotResult = dot(dir, in_normal);
    ////        dotSums += abs(dotResult);
    ////    }
    ////    float dotMean = dotSums/in_directionLights.length();
    //
    //    vec3 dir = in_directionLights[0].xyz;
    //    float dotResult = dot(dir, in_normal);
    //    v2f_shade = abs(dotResult);
    
    
}