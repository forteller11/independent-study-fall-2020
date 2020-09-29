in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

out vec2 v2f_uv;
out vec3 v2f_diffuse;
out vec3 v2f_specular;
out vec3 v2f_worldNorm;
out vec3 v2f_worldPos;

uniform mat4 ModelToWorld;
uniform mat4 WorldToView;
uniform mat4 ModelToView;
uniform mat4 ModelRotation;


void main()
{
    v2f_worldPos =  (vec4(in_position, 1) * ModelToWorld).xyz;
    v2f_worldNorm = (vec4(in_normal, 1) * ModelRotation).xyz;
    
    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;

    v2f_diffuse = calculate_diffuse(v2f_worldNorm, v2f_worldPos);
    gl_Position = viewPos;
    v2f_uv = in_uv;
}

