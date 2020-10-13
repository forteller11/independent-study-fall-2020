out vec4 fragColor;

in vec2 v2f_uv;

uniform sampler2D MainTexture;

void main()
{
    fragColor = texture(MainTexture, v2f_uv);
}