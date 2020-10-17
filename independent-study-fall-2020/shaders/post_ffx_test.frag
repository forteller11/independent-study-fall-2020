layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;
const float MAX_OFFSET = 0.02;

void main(){

    //float depth = texture(MainDepth, vec3(v2f_uv,1), 0.01);
    //vec4 depthColor = texture(MainDepth, v2f_uv);
    //vec4 color = texture(MainColor, v2f_uv);
    //frag_color = vec4(vec3(depth), 1);
    float depth = texture(SecondaryColor, v2f_uv).r;
    float offsetDynamic = mix(0, MAX_OFFSET, 1-depth);
    
   float r = texture(MainColor, v2f_uv + vec2(offsetDynamic, offsetDynamic)).r;
    float g = texture(MainColor, v2f_uv ).g;
    float b = texture(MainColor, v2f_uv + vec2(-offsetDynamic, offsetDynamic)).b;

//    float r = 1;
//    float g = 0;
//    float b = 1;
    MainFragColor = vec4(r,g,b,1);
    SecondaryFragColor = vec4(1,0,0,1);
}