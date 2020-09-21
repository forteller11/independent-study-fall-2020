#version 330 core
in vec3 in_position;
in vec2 in_uv;
uniform mat4 Transform;
uniform mat4 Rotation;
uniform vec3 CamPosition;

out vec2 uv;
void main()
{
    uv = in_uv;
    vec4 worldPos = vec4(in_position - CamPosition, 1.0);
    vec4 worldRot = Rotation * worldPos;

    vec4 projectedPos = Transform * worldRot;
    gl_Position = projectedPos;
}