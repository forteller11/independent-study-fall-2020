in vec3 in_position;
in vec3 in_normal;

out vec3 v2f_normal;

uniform mat4 ModelToWorld;
uniform mat4 WorldToView;
uniform mat4 ModelToView;
uniform mat3 ModelRotation;


void main()
{
    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;

    gl_Position = viewPos;
    v2f_normal = in_normal;
}

