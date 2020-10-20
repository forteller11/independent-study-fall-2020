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
uniform float Time;


void main()
{
    //NOTE: FOLLOWING CODE FROM: https://learnopengl.com/Advanced-Lighting/Shadows/Shadow-Mapping
    // perform perspective divide
    vec3 projCoords = v2f_viewPosLightSpace.xyz / v2f_viewPosLightSpace.w;
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5;
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(ShadowMap, projCoords.xy).r;
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // check whether current frag pos is in shadow
    vec3 lightDir = vec3(0,-1,0);
    float bias = max(0.05 *(1.0 - abs(dot(v2f_norm, lightDir))), 0.005);
//    float bias = 0.005;
    float distBetweenShadowAndFragDepth = currentDepth - closestDepth;
    int shadow = currentDepth - bias > closestDepth  ? 1 : 0;
    float shadowMult = max(0.2, 1-float(shadow));

    vec4 normalMapNoise = texture(Color, v2f_uv, 4);
    
    vec2 uv = shadow == 1 ?
    v2f_uv + (vec2(normalMapNoise.r * sin(Time/1.47), normalMapNoise.r * cos(Time*1.36)) * .005)
    : v2f_uv;
    

    //------------------------------------------------------------------

    
    vec4 diffuseTex = texture(Color, uv);
    vec4 normalMapTex = texture(Normal, uv);
    vec4 glossMapTex = texture(Gloss, uv);

    vec3 normalsWithMapWorld = normal_map_world_space(normalMapTex.xyz, v2f_tangentToModelSpace, mat3(ModelRotation), v2f_norm);

    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition, glossMapTex.x * NormalMapStrength, SpecularRoughness);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);
    
    vec3 texColorShaded = diffuseTex.xyz * (diffuse + specular) * shadowMult;
    


    

    MainFragColor = vec4(texColorShaded.xyz, 1);


    
    SecondaryFragColor = vec4(vec3(gl_FragCoord.z/gl_FragCoord.w), 1);
}