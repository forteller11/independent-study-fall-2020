struct FrustrumStruct{
    vec3 Position;
    
    mat3 Rotation;
    mat3 RotationInverse;
    
    float NearClip;
    float FarClip;
    
    float NearClipWidth;
    float FarClipWidth;
};