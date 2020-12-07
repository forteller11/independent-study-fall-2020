layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;

void main(){
    
    vec4 texColor;

    texColor = texture(MainTexture, v2f_uv);
    texColor = mix(texColor, vec4(.6,.65,.8,1), 0.2);
    


//    texColor = texture(MainTexture2, vec2(0.5,0.5));
    
    
//    vec4 texColor1 = texture(MainTexture1, v2f_uv);
//    float texDepth = texture(SecondaryTexture1, v2f_uv).r;
//    vec4 texColor2 = texture(MainTexture2, v2f_uv);
//    vec4 texColor = mix(texColor1, texColor2, 0.5);
    
    MainFragColor = vec4(texColor.rgb, 1);
    SecondaryFragColor =  vec4(vec3(FragCoordToDepth(gl_FragCoord)), 1);
//    MainFragColor = vec4(v2f_uv,0,1);
//    SecondaryFragColor = vec4(texDepth);
}