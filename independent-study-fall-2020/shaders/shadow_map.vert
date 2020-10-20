uniform mat4 ModelToViewLight;
void main()
{
    vec4 viewPos =   vec4(in_position, 1f) * ModelToViewLight;
    gl_Position = viewPos;
}

