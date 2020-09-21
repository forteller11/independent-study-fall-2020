#version 330 core
in vec3 in_position;
in vec2 in_uv;
//in mat4 in_transform;
uniform mat4 transform;

out vec2 uv;
void main()
{
    uv = in_uv;
    vec4 newPos = transform * vec4(in_position, 1.0);
    gl_Position = newPos;
}