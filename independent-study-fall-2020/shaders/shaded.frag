out vec4 fragColor;

in vec2 v2f_uv;
in vec3 v2f_diffuse;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;

uniform sampler2D Color;

uniform vec3 CamPosition;

uniform mat4 ModelRotation;

void main()
{
    vec4 diffuseColor = texture(Color, v2f_uv);
    
    vec3 specular = calculate_specular(v2f_worldNorm, v2f_worldPos, CamPosition);
    
    vec3 texColorShaded = diffuseColor.xyz * (v2f_diffuse + specular);

    fragColor = vec4(texColorShaded, 1);
}