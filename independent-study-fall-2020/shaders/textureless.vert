#version 330 core
in vec3 in_position;
in vec3 in_uv;

out vec2 v2f_uv;
//in vec4 in_color;

uniform mat4 ModelToWorld;
uniform mat4 WorldToView;
//uniform mat4 Rotation;
//uniform vec3 CamPosition;

//out vec4 color;
void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;
    
    gl_Position = viewPos;
    v2c_uv = in_uv;

//    color = in_color;
}