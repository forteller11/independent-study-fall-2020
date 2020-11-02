layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;

uniform sampler2D MainTexture1;

uniform sampler2D MainTexture2;
uniform sampler2D SecondaryTexture1;
uniform sampler2D SecondaryTexture2;

uniform int TextureIndex;
void main(){
    
    vec4 texColor;
    
    
    if (TextureIndex == 0){
        texColor = texture(MainTexture1, v2f_uv);
    }

    else if (TextureIndex == 1){
        texColor = texture(MainTexture2, v2f_uv);
        vec4 a = vec4(0,1,0,1);
        texColor = mix(a, texColor, 0.8);
    }
    
    else {
        texColor = vec4(0,0,1,1);
    }
//    texColor = texture(MainTexture2, vec2(0.5,0.5));
    
    
//    vec4 texColor1 = texture(MainTexture1, v2f_uv);
//    float texDepth = texture(SecondaryTexture1, v2f_uv).r;
//    vec4 texColor2 = texture(MainTexture2, v2f_uv);
//    vec4 texColor = mix(texColor1, texColor2, 0.5);
    
    MainFragColor = vec4(texColor.rgb, 1);
    SecondaryFragColor = vec4(texture(SecondaryTexture1, v2f_uv).rgb, 1);
//    MainFragColor = vec4(v2f_uv,0,1);
//    SecondaryFragColor = vec4(texDepth);
}