out vec4 MainFragColor;
out vec4 SecondaryFragColor;

in vec3 v2f_normal;

void main()
{
    MainFragColor = vec4(vec3(gl_FragCoord.z), 1);
    SecondaryFragColor = vec4(1,0,0,1);
}