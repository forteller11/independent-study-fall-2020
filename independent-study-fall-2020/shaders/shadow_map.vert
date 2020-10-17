out vec3 v2f_normal;

void main()
{
    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;

    gl_Position = viewPos;
    v2f_normal = in_normal;
}

