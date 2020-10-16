//If a texture has a depth or depth-stencil image format and has the depth comparison activated, it cannot be used with a normal sampler. Attempting to do so results in undefined behavior. Such textures must be used with a shadow sampler. This type changes the texture lookup functions (see below), adding an additional component to the textures' usual texture coordinate vector. This extra value is used to compare against the value sampled from the texture.

out vec4 frag_color;
in vec2 v2f_uv;

uniform sampler2D MainColor;
uniform sampler2D MainDepth;

void main(){

    //float depth = texture(MainDepth, vec3(v2f_uv,1), 0.01);
    //vec4 depthColor = texture(MainDepth, v2f_uv);
    //vec4 color = texture(MainColor, v2f_uv);
    //frag_color = vec4(vec3(depth), 1);
    frag_color = vec4(1,0,0,.2);

}