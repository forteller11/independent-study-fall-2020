﻿layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

uniform vec4 Color;
void main()
{
    MainFragColor = Color;
    SecondaryFragColor =  vec4(vec3(FragCoordToDepth(gl_FragCoord)), 1);
}