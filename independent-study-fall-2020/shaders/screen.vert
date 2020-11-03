﻿out vec2 v2f_uv;

void main()
{
    vec4 worldPos =  vec4(in_position, 1f) * ModelToWorld;
    vec4 viewPos =   worldPos * WorldToView;

    gl_Position = viewPos;
    //gl_Position = vec4(in_position, 1);
    v2f_uv = in_uv;
}