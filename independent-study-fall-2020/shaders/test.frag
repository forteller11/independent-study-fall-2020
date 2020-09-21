#version 330 core

//todo v2f naming coventions
out vec4 fragColor;

in vec2 uv;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main()
{
    vec4 texMap1 = texture(texture0, uv);
    vec4 texMap2 = texture(texture1, uv);
    fragColor = mix(texMap1, texMap2, 0.7f);
//    fragColor = vec4(transform[3][3], 0, 0, 1);
}