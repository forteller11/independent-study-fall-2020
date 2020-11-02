layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

in vec2 v2f_uv;

uniform sampler2D Room2Texture;

const float MAX_OFFSET = 0.01;

void main(){
    vec4 tex = texture(Room2Texture, v2f_uv);
    
    
    float depth = texture(SecondaryTexture, v2f_uv).r;

    float offsetDynamic = mix(-.05, MAX_OFFSET, depth);
    offsetDynamic = max(0, offsetDynamic);

    
    vec4 color = texture(MainTexture, v2f_uv);
    float r = texture(MainTexture, v2f_uv + vec2(offsetDynamic, offsetDynamic)).r;
    float g = texture(MainTexture, v2f_uv ).g;
    float b = texture(MainTexture, v2f_uv + vec2(-offsetDynamic, offsetDynamic)).b;
    
    depth = min(depth*2, 1);
    vec4 colorFog = color * depth;
    MainFragColor = vec4(r,g,b, 1);
   // MainFragColor = vec4(tex.rgb, 1);
//    MainFragColor = vec4(vec3(simplex3d(vec3(normScreenCoord.xy*60, 0))), 1);
    
    SecondaryFragColor = vec4(1,0,0,1);
}