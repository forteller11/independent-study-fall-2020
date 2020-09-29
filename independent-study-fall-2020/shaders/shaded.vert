in vec3 in_position;
in vec2 in_uv;
in vec3 in_normal;

out vec2 v2f_uv;
out vec3 v2f_diffuse;

uniform mat4 ModelToWorld;
uniform mat4 ModelToView;
uniform mat4 ModelRotation;


void main()
{
    vec4 worldPos =  vec4(in_position, 1) * ModelToWorld;
    vec4 viewPos =   vec4(in_position, 1) * ModelToView;
    vec3 worldNorm = (vec4(in_normal, 1) * ModelRotation).xyz;

    v2f_diffuse = calculate_diffuse(worldNorm, worldPos);
    gl_Position = viewPos;
    v2f_uv = in_uv;
}

