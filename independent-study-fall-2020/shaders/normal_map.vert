

out vec2 v2f_uv;
out vec3 v2f_diffuse;
out vec3 v2f_specular;
out vec3 v2f_worldNorm;
out vec3 v2f_norm;
out vec3 v2f_worldPos;
out mat3 v2f_tangentToModelSpace;

void main()
{
    v2f_worldPos =  (vec4(in_position, 1) * ModelToWorld).xyz;
    v2f_worldNorm = in_normal * mat3(ModelRotation);
    v2f_norm = in_normal;

    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;
    
    v2f_tangentToModelSpace = calculate_tanToModel_space(in_normal);
    
    gl_Position = viewPos;
    v2f_uv = in_uv;
}

