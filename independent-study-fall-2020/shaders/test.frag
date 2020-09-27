#version 330 core

//todo v2f naming coventions
out vec4 fragColor;

in vec2 v2f_uv;

uniform sampler2D texture0;
uniform sampler2D texture1;

void main()
{
    vec4 texMap1 = texture(texture0, v2f_uv);
    vec4 texMap2 = texture(texture1, v2f_uv);
    fragColor = mix(texMap1, texMap2, 0.5f);
//    fragColor = vec4(transform[3][3], 0, 0, 1);
}