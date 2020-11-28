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

uniform bool VisibleInFrustrum;

uniform float NormalMapStrength;
uniform float SpecularRoughness;

uniform FrustrumStruct Frustrum;


void main()
{

    bool inFrustrum = IsPointWithinFrustrum(v2f_worldPos, Frustrum);
    
    if (inFrustrum == VisibleInFrustrum){
        discard;
    }

    vec3 lightDir = vec3(0,-1,0);
    vec2 shadowBias = vec2(0.005,0.05);
    int inShadow = shadow_map(v2f_viewPosLightSpace, v2f_worldNorm, lightDir, shadowBias);

    float shadowMult = max(0.2, 1-float(inShadow)); //todo change intensity based on difference
    vec4 normalMapNoise = texture(Color, v2f_uv, 4);

    vec3 noiseInput = vec3(v2f_uv.xy*8, Globals.TimeAbs/2.5);
    vec3 noiseOffset = vec3(0,0,99999);
    float u = simplex3d(noiseInput);
    float v = simplex3d(noiseInput+noiseOffset);
    vec2 uvOffset = vec2(u,v) * 0.007;

    vec2 uv = inShadow == 1 ?
    v2f_uv + uvOffset
    : v2f_uv;
//    if (inFrustrum != ShouldAppearInFrustrum){
//        discard;
//    }
    
    vec4 diffuseTex = texture(Color, v2f_uv);
    vec4 normalMapTex = texture(Normal, v2f_uv);
    vec4 glossMapTex = texture(Gloss, v2f_uv);

    vec3 normalsWithMapWorld = normal_map_world_space(normalMapTex.xyz, v2f_tangentToModelSpace, mat3(ModelRotation), v2f_norm);

    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition, glossMapTex.x * NormalMapStrength, SpecularRoughness);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);
    
    
    vec3 texColorShaded = diffuseTex.xyz * (diffuse + specular+ vec3(.15)) ;
    
    MainFragColor = vec4(texColorShaded.xyz, 1);
    SecondaryFragColor = vec4(vec3(FragCoordToDepth(gl_FragCoord)), 1);
}