
uniform sampler2D ShadowMap;
//NOTE: FOLLOWING CODE FROM: https://learnopengl.com/Advanced-Lighting/Shadows/Shadow-Mapping
int shadow_map(
vec4 lightSpaceViewPos, 
 vec3 worldNormal, 
 vec3 lightDirection, 
 vec2 biasMinMax){
    // perform perspective divide
    vec3 projCoords = lightSpaceViewPos.xyz / lightSpaceViewPos.w;
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5;
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
//    vec2 uvOffset = texture(NoiseTexture, gl_FragCoord.xy/1440, 0).rg;
//    uvOffset *= 0.005;
    
    float closestDepth = texture(ShadowMap, projCoords.xy).r;
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // check whether current frag pos is in shadow
    float bias = max(biasMinMax.y *(1.0 - abs(dot(worldNormal, lightDirection))), biasMinMax.x);
//    float bias = 0.005;
    float distBetweenShadowAndFragDepth = currentDepth - closestDepth;
    int shadow = currentDepth - bias > closestDepth  ? 1 : 0;
    return shadow;
}

vec2 get_shadow_offset(vec2 uv){ 
    vec3 noiseInput = vec3(uv.xy*12, Globals.TimeAbs/1);
    vec3 noiseOffset = vec3(0,0,99999);
    float u = simplex3d(noiseInput);
    float v = simplex3d(noiseInput+noiseOffset);
    vec2 uvOffset = vec2(u,v) * 0.007;
    return uvOffset;
}