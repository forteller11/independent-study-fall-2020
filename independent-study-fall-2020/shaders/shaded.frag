﻿layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;
in vec3 v2f_diffuse;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;

uniform sampler2D Color;

uniform vec3 CamPosition;

uniform mat4 ModelRotation;

void main()
{
    vec4 diffuseColor = texture(Color, v2f_uv);
    
    vec3 specular = calculate_specular(v2f_worldNorm, v2f_worldPos, CamPosition, 0.8f, 32);
    
    vec3 texColorShaded = diffuseColor.xyz * (v2f_diffuse + specular);

    MainFragColor = vec4(texColorShaded, 1);
    SecondaryFragColor = vec4(1,0,0,1);
}