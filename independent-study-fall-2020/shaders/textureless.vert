in vec3 in_position;

//in vec4 in_color;

uniform mat4 ModelToWorld;
uniform mat4 ModelToView;
uniform mat4 Rotation;

//out vec4 color;
void main()
{
//    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   vec4(in_position, 1f) * ModelToView;
    
    gl_Position = viewPos;

//    color = in_color;
}