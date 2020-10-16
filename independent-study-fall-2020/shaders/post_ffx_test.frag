//If a texture has a depth or depth-stencil image format and has the depth comparison activated, it cannot be used with a normal sampler. Attempting to do so results in undefined behavior. Such textures must be used with a shadow sampler. This type changes the texture lookup functions (see below), adding an additional component to the textures' usual texture coordinate vector. This extra value is used to compare against the value sampled from the texture.

out vec4 frag_color;
in vec2 v2f_uv;

uniform sampler2D MainColor;
uniform sampler2DShadow MainDepth;

void main(){
    
    //float depth = texture(MainDepth, v2f_uv); //am i going get values between 1 and 0??????... i dont want that... i want depth values..... REDO
    vec4 color = texture(MainColor, v2f_uv);
    frag_color = vec4(v2f_uv.xy, 0, 1);
    frag_color = color;
}