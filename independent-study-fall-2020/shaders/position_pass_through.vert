//for use in post-ffx shaders
in vec3 in_position;
in vec2 in_uv;

out vec2 v2f_uv;

void main()
{
    gl_Position = vec4(in_position,1);
    v2f_uv = in_uv;
}

