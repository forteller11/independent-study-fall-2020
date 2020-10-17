layout (location = 0) out vec4 MainFragColor;
layout (location = 1) out vec4 SecondaryFragColor;

uniform sampler2D MainTexture;

void main()
{
    MainFragColor = texture(MainTexture, v2f_uv);
    SecondaryFragColor = vec4(1,0,0,1);
}