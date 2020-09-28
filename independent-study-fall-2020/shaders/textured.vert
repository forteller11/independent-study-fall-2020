#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec4 in_directionLights [];
in vec4 in_spotLights [];


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
    
    
}