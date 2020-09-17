#version 330 core
in vec3 in_position;
in vec2 in_uv;

out vec2 uv;
void main()
{
    uv = in_uv;
    gl_Position = vec4(positions, 1.0);
}