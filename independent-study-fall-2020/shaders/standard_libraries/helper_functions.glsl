float FragCoordToDepth(vec4 fragCoord){
    return fragCoord.z/fragCoord.w;
}

float GetPercentageBetweenEdges(float edge1, float edge2, float value)
{
    float sum = edge2 - edge1;
    float valueWithEdge1Origin = value - edge1;
    return valueWithEdge1Origin / sum;
}
        
vec3 PivotAbout(vec3 point, vec3 pivot, mat3 rotation)
{
    vec3 pointWithPivotOrigin = point - pivot;
    vec3 pointRotated = rotation * pointWithPivotOrigin;
    return pointRotated + pivot;
}
        
bool IsPointWithinFrustrum(vec3 point, FrustrumStruct frustrum){

    vec3 pointAlignedSpace = PivotAbout(point, frustrum.Position, frustrum.RotationInverse);
    vec3 nearCenterAlignedSpace = vec3(0, 0, frustrum.NearClip) - frustrum.Position;
    vec3 farCenterAlignedSpace  = vec3(0, 0, frustrum.FarClip)  - frustrum.Position;

    float t = GetPercentageBetweenEdges(nearCenterAlignedSpace.z, farCenterAlignedSpace.z, - pointAlignedSpace.z);

    float widthOfFrustrumAtPoint = mix(frustrum.NearClipWidth, frustrum.FarClipWidth, t);

    vec3 pointFrustrumOrigin = pointAlignedSpace - frustrum.Position;

    if (abs(pointFrustrumOrigin.x) > widthOfFrustrumAtPoint / 2)
        return false;
    if (abs(pointFrustrumOrigin.y) > widthOfFrustrumAtPoint / 2)
        return false;

    return true;
}