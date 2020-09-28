#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

#define NR_LIGHTS 4

struct DirectionLight{
    vec3 Color;
    vec3 Direction;
};

struct PointLight{
    vec3 Color;
    vec3 Position;
};

uniform DirectionLight DirectionLights [NR_LIGHTS];
uniform int DirectionLightsLength;

uniform PointLight PointLights [NR_LIGHTS];
uniform int PointLightsLength;

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

    v2f_diffuse = vec3(0,0,0);
    for (int i = 0; i < DirectionLightsLength; i++){
        v2f_diffuse += DirectionLights[i].Color;
    }
 
}
