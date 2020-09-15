#version 330 core
in vec3 vertPositions;

void main()
{
    gl_Position = vec4(vertPositions, 1.0);
}