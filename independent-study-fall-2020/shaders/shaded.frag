
#version 330 core

//todo v2f naming coventions
out vec4 fragColor;

in vec2 v2f_uv;
in float v2f_shade;
in vec3 v2f_normal_world;

uniform sampler2D texture0;
uniform sampler2D texture1;

in vec4 in_directionLights[];

void main()
{
    vec4 texMap1 = texture(texture0, v2f_uv);
    vec4 texMap2 = texture(texture1, v2f_uv);
    vec4 texColor = mix(texMap1, texMap2, 0.5f);
    
    vec4 texColorShaded = vec4(texColor.xyz * v2f_shade * v2f_normal_world, 1);;
    fragColor = texColorShaded;
//    fragColor = in_directionLights[0];
    //    fragColor = vec4(v2f_uv.x, v2f_uv.y, 0, 1);
}