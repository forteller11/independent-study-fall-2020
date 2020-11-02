layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;

void main(){
    vec4 texColor = texture(MainTexture, v2f_uv);
    float texDepth = texture(SecondaryTexture, v2f_uv).r;
    
    MainFragColor = vec4(texColor.rgb, 1);
    SecondaryFragColor = vec4(texDepth);
}