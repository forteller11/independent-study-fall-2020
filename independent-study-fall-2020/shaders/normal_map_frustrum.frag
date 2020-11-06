layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;
in vec3 v2f_norm;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;
in mat3 v2f_tangentToModelSpace;
in vec3 v2f_viewPosNoProjection;

in vec4 v2f_viewPos;

uniform sampler2D Color;
uniform sampler2D Normal;
uniform sampler2D Gloss;

uniform float NormalMapStrength;
uniform float SpecularRoughness;
uniform FrustrumStruct Frustrum;


void main()
{
    if (IsPointWithinFrustrum(v2f_worldPos, Frustrum)){
        discard;
    }
    
    vec4 diffuseTex = texture(Color, v2f_uv);
    vec4 normalMapTex = texture(Normal, v2f_uv);
    vec4 glossMapTex = texture(Gloss, v2f_uv);

    vec3 normalsWithMapWorld = normal_map_world_space(normalMapTex.xyz, v2f_tangentToModelSpace, mat3(ModelRotation), v2f_norm);

    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition, glossMapTex.x * NormalMapStrength, SpecularRoughness);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);
    
    
    vec3 texColorShaded = diffuseTex.xyz * (diffuse + specular);
    
    MainFragColor = vec4(texColorShaded.xyz, 1);
    SecondaryFragColor = vec4(vec3(FragCoordToDepth(gl_FragCoord)), 1);
}