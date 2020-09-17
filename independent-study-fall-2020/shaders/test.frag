#version 330 core

out vec4 fragColor;

in vec2 uv;

uniform sampler2D textureSampler;

void main()
{
//    fragColor = texture(textureSampler, uv);
    fragColor = vec4(uv.x,uv.y,0,1);
}