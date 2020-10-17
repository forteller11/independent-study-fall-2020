out vec4 fragColor;

in vec2 v2f_uv;
in vec3 v2f_norm;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;
in mat3 v2f_tangentToModelSpace;

uniform sampler2D Color;
uniform sampler2D Normal;
uniform sampler2D Gloss;
uniform sampler2D ShadowMap;

uniform float NormalMapStrength;

uniform float SpecularRoughness;

void main()
{
    vec4 diffuseTex = texture(Color, v2f_uv);
    vec4 normalMapTex = texture(Normal, v2f_uv);
    vec4 glossMapTex = texture(Gloss, v2f_uv);
    vec4 shadowMapTex = texture(ShadowMap, v2f_uv);

    vec3 normalMapModelSpace = calculate_norm_map_model_space(normalMapTex.xyz, v2f_tangentToModelSpace);
    vec3 normalsWithMapModel = normalize(v2f_norm + (normalMapModelSpace ));
    vec3 normalsWithMapWorld = normalsWithMapModel * mat3(ModelRotation);

    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition, glossMapTex.x * NormalMapStrength, SpecularRoughness);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);

    vec3 texColorShaded = diffuseTex.xyz * (diffuse + specular);

    fragColor = vec4(texColorShaded.xyz, 1);
}