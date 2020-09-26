#version 330 core
in vec3 in_position;
uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos = vec4(in_position - CamPosition, 1.0);
    vec4 worldRot = Projection * worldPos;

    vec4 projectedPos = Transform * worldRot;
    gl_Position = projectedPos;
}