out vec4 fragColor;

in vec3 v2f_normal;


uniform float SpecularRoughness;

void main()
{

    fragColor = vec4(v2f_normal, 1);
}