out vec4 MainFragColor;
out vec4 SecondaryFragColor;

in vec2 v2f_uv;
const float offset = 0.01;

void main(){

    //float depth = texture(MainDepth, vec3(v2f_uv,1), 0.01);
    //vec4 depthColor = texture(MainDepth, v2f_uv);
    //vec4 color = texture(MainColor, v2f_uv);
    //frag_color = vec4(vec3(depth), 1);
    
   float r = texture(MainColor, v2f_uv + vec2(offset, offset)).r;
    float g = texture(MainColor, v2f_uv ).g;
    float b = texture(MainColor, v2f_uv + vec2(-offset, -offset)).b;

//    float r = 1;
//    float g = 0;
//    float b = 1;
    MainFragColor = vec4(r,g,b,1);
    SecondaryFragColor = vec4(1,0,0,1);
}