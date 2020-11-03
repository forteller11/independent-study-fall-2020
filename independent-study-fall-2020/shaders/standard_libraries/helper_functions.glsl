float FragCoordToDepth(vec4 fragCoord){
    return fragCoord.z/fragCoord.w;
}