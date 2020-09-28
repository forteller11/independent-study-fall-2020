#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec2 in_normal;

in vec4 in_directionLights [4];
in vec4 in_spotLights [4];


out vec2 v2f_uv;
out float v2f_shade;

uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    v2f_uv = in_uv;

    float dotSums = 0;
    for (int  i; i < in_directionLights.length(); i++){
        vec3 dir = in_directionLights[i].xyz;
        float dotResult = dot(dir, in_normal);
        dotSums += abs(dotResult);
    }
    float dotMean = dotSums/in_directionLights.length();
    v2f_shade = dotMean;
}