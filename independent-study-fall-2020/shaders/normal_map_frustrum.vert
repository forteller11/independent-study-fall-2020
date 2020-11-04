out vec2 v2f_uv;
out vec3 v2f_diffuse;
out vec3 v2f_specular;
out vec3 v2f_worldNorm;
out vec3 v2f_viewPosNoProjection;
out vec3 v2f_norm;
out vec3 v2f_worldPos;
out mat3 v2f_tangentToModelSpace;

out vec4 v2f_viewPos;

uniform mat4 ModelToViewLight;

void main()
{
    v2f_worldPos =  (vec4(in_position, 1) * ModelToWorld).xyz;
    v2f_worldNorm =  mat3(ModelRotation) * in_normal ;
    v2f_norm = in_normal;

    vec4 viewPos =  vec4(in_position, 1f) * ModelToView;
    
    v2f_viewPosNoProjection =  (vec4(v2f_worldPos, 1) * ModelToWorldNoProjection).xyz;
    
    v2f_tangentToModelSpace = calculate_tanToModel_space(in_normal);
    
    v2f_uv = in_uv;

    gl_Position = viewPos;
    v2f_viewPos =  viewPos;

}

