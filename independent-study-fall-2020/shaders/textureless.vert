#version 330 core
in vec3 in_position;
//in vec4 in_color;

uniform mat4 WorldToView;
uniform mat4 ModelToWorld;
//uniform mat4 Rotation;
uniform vec3 CamPosition;

//out vec4 color;
void main()
{
    vec4 worldPos = ModelToWorld * vec4(in_position,1);
    vec4 viewPos = WorldToView * worldPos;
    
    gl_Position = viewPos;

//    color = in_color;
}