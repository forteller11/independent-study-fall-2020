layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;
const float MAX_OFFSET = 0.015;

void main(){

    //float depth = texture(MainDepth, vec3(v2f_uv,1), 0.01);
    //vec4 depthColor = texture(MainDepth, v2f_uv);
    //vec4 color = texture(MainColor, v2f_uv);
    //frag_color = vec4(vec3(depth), 1);
    
    float depth = texture(SecondaryTexture, v2f_uv).r;
    if (depth >= .99){
        depth = 0;
    }
    if (depth > .8 && depth < .99){
        depth = 1;
    }

//    float noise = rand(gl_FragCoord.xy);
//    MainFragColor = vec4(vec3(rand(gl_FragCoord.xy)), 1);
    
    vec2 normScreenCoord = gl_FragCoord.xy/1440;
    float offsetDynamic = mix(0, MAX_OFFSET, 1-depth);

    
    vec4 color = texture(MainTexture, v2f_uv);
   float r = texture(MainTexture, v2f_uv + vec2(offsetDynamic, offsetDynamic)).r;
    float g = texture(MainTexture, v2f_uv ).g;
    float b = texture(MainTexture, v2f_uv + vec2(-offsetDynamic, offsetDynamic)).b;
    
    depth = min(depth*2, 1);
    vec4 colorFog = color * depth;
    MainFragColor = vec4(r,g,b, 1);
    MainFragColor = vec4(vec3(simplex3d(vec3(normScreenCoord.xy*60, 0))), 1);
    
    SecondaryFragColor = vec4(1,0,0,1);
}