out vec4 fragColor;

in vec3 v2f_normal;


void main()
{

    fragColor = vec4(vec3(gl_FragCoord.z), 1);
}