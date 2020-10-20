layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;
in vec3 v2f_norm;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;
in mat3 v2f_tangentToModelSpace;
in vec3 v2f_viewPosNoProjection;

in vec4 v2f_viewPos;
in vec4 v2f_viewPosLightSpace;

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

    vec3 normalsWithMapWorld = normal_map_world_space(normalMapTex.xyz, v2f_tangentToModelSpace, mat3(ModelRotation), v2f_norm);
    
    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition, glossMapTex.x * NormalMapStrength, SpecularRoughness);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);

    vec3 texColorShaded = diffuseTex.xyz * (diffuse + specular);


    // perform perspective divide
    vec3 projCoords = v2f_viewPosLightSpace.xyz / v2f_viewPosLightSpace.w;
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5;
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(ShadowMap, projCoords.xy).r;
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // check whether current frag pos is in shadow
    float bias = 0.008;
    float shadow = currentDepth - bias > closestDepth  ? 1.0 : 0.0;


    MainFragColor = vec4(texColorShaded.xyz*(1-shadow), 1);


    
    SecondaryFragColor = vec4(vec3(1), 1);
}