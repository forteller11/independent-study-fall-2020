#version 330 core

//todo v2f naming coventions
out vec4 fragColor;

in vec2 uv;

uniform sampler2D texture1;

void main()
{
    vec4 texMap = texture(texture1, uv);
    vec4 uvVisualize = vec4(uv.x,uv.y,0,1);
    fragColor = (texMap + uvVisualize )/2;
    fragColor = texMap;
}