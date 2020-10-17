vec3 calculate_norm_map_model_space(vec3 normalMapColor, mat3 tangentToModelSpace){
    vec3 normalMapTangentSpace = ((normalMapColor * 2) - 1).xyz;
    vec3 normalMapModelSpace = normalMapTangentSpace * tangentToModelSpace;
    return normalMapModelSpace;
}

mat3 calculate_tanToModel_space(vec3 modelNormal){
    vec3 ortho1 =  cross(vec3(1,0,0), modelNormal);
    vec3 ortho2 =  cross(ortho1, modelNormal);
    return mat3(ortho2, ortho1, modelNormal);
}