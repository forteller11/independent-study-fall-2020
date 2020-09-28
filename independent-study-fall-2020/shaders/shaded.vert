﻿#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

uniform vec3 PointLightsPositions [4];
uniform vec3 PointLightsColors [4];
uniform int  PointLightsLength;

uniform vec3 DirectionLightsDirections[4]; //NOTE holy canoli the SECOND ELEMENT is getting optimized out...
uniform vec3 DirectionLightsColors[4];
uniform int  DirectionLightsLength;

out vec2 v2f_uv;
out vec3 v2f_diffuse;

uniform mat4 ModelRotation;
uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    v2f_uv = in_uv;

    
    vec3 diffuseColorSum = vec3(0,0,0);
    for (int i = 0; i < 4; i++){
        float product = dot(DirectionLightsDirections[i], normal_world);
        float diffuseShade = clamp(product, 0, 1);
        vec3 diffuseColor = diffuseShade * DirectionLightsColors[i];
        diffuseColorSum += diffuseColor;
    }
}
