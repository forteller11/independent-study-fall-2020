#version 330 core
in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

//uniform vec3 PointLightsPositions [4];
//uniform vec3 PointLightsColors [4];
//uniform int  PointLightsLength;

uniform vec3 DirectionLightsDirections [4];
uniform vec3 DirectionLightsColors [4];
uniform int  DirectionLightsLength;

out vec2 v2f_uv;
out vec3 v2f_diffuse;
out vec3 v2f_normal_world;

uniform mat4 ModelRotation;
uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    v2f_uv = in_uv;

    v2f_normal_world = (vec4(in_normal, 1f) * ModelRotation).xyz;
    
    vec3 diffuseColorSum = vec3(0,0,0);
    for (int i = 0; i < 4; i++){
        float product = dot(DirectionLightsDirections[i], v2f_normal_world);
        float diffuseShade = clamp(product, 0, 1);
        vec3 diffuseColor = product * DirectionLightsColors[i];
        diffuseColorSum += diffuseColor;
    }
    v2f_diffuse = clamp(diffuseColorSum, 0, 1);
}