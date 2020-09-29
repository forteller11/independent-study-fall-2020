in vec3 in_position;

uniform mat4 ModelToView;

void main()
{
    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;
    gl_Position = viewPos;
}