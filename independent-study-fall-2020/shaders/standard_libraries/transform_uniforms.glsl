
//layout (std140) uniform Transform
//{
uniform mat4 ModelToView; //4*4*4
uniform mat4 WorldToView; //128
uniform mat4 ModelToWorld; //
uniform mat4 ModelRotation;
uniform mat4 ModelToWorldNoProjection;
uniform vec3 CamPosition; //4x4

//64x5 + 16 = 336
//};