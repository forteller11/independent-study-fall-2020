layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

void main()
{
    MainFragColor = vec4(vec3(gl_FragCoord.z), 1);
    SecondaryFragColor = vec4(1,0,0,1);
}