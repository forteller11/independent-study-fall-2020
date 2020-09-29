out vec4 fragColor;

in vec2 v2f_uv;
in vec3 v2f_diffuse;
in vec3 v2f_specular;

in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;

uniform sampler2D texture0;
uniform sampler2D texture1;

uniform vec3 CamPosition;

void main()
{
    vec4 texMap1 = texture(texture0, v2f_uv);
    vec4 texMap2 = texture(texture1, v2f_uv);
    vec4 texColor = mix(texMap1, texMap2, 0);
    
    vec3 texColorShaded = texColor.xyz * v2f_diffuse;
    vec3 specular = calculate_specular(v2f_worldNorm, v2f_worldPos, CamPosition);
    texColorShaded = specular;
//    texColorShaded *= DirectionLights[0];
    fragColor = vec4(texColorShaded, 1);
}