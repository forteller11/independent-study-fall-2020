in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

out vec2 v2f_uv;
out vec3 v2f_diffuse;

#define NR_LIGHTS 4
uniform mat4 ModelRotation;
uniform mat4 ModelToWorld;
uniform mat4 WorldToView;


void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    v2f_uv = in_uv;

    vec3 worldNormal = (vec4(in_normal, 1) * ModelRotation).xyz;
    v2f_diffuse = calculate_diffuse(worldNormal);
}

