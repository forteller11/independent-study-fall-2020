out vec4 fragColor;

uniform sampler2D MainTexture;

void main()
{
    fragColor = texture(MainTexture, v2f_uv);
}