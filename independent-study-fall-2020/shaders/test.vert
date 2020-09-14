#version 330 core
layout (location = 0) in vec3 screenPosition;

void main()
{
    gl_Position = vec4(screenPosition, 1.0);
}