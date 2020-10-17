out vec4 MainFragColor;
out vec4 SecondaryFragColor;

uniform vec4 Color;
void main()
{
    MainFragColor = Color;
    SecondaryFragColor = vec4(1,0,0,1);
}