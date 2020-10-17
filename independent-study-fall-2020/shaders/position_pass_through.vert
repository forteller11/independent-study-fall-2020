//for use in post-ffx shaders
out vec2 v2f_uv;

void main()
{
    gl_Position = vec4(in_position,1);
    v2f_uv = in_uv;
}

