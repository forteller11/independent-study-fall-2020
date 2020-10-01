out vec4 fragColor;

in vec2 v2f_uv;
in vec3 v2f_norm;
in vec3 v2f_worldNorm;
in vec3 v2f_worldPos;

uniform sampler2D Color;
uniform sampler2D Normal;

uniform vec3 CamPosition;

uniform mat4 ModelRotation;

void main()
{
    vec4 diffuseColor = texture(Color, v2f_uv);
    vec4 normalMapTex = texture(Normal, v2f_uv);

    float normalMapContribution = .2f;
    //todo transform norms from tangent space with TBN matrix
    vec3 normalMapTangentSpace = ((normalMapTex * 2) - 1).xyz;

    vec3 ortho1 =  cross(vec3(1,0,0), v2f_norm);
    vec3 ortho2 =  cross(ortho1, v2f_norm);
    mat3 tangentToModelSpace = mat3(v2f_norm, ortho1, ortho2);
    vec3 normalMapModelSpace = normalMapTangentSpace * tangentToModelSpace;
    vec3 normalsWithMapModel = normalize(v2f_worldNorm + (normalMapModelSpace * normalMapContribution));
    vec3 normalsWithMapWorld = normalsWithMapModel * mat3(ModelRotation);
    //    vec4 texColor = mix(texMap1, texMap2, 0f);

    vec3 specular = calculate_specular(normalsWithMapWorld, v2f_worldPos, CamPosition);
    vec3 diffuse = calculate_diffuse(normalsWithMapWorld, v2f_worldPos);

    vec3 texColorShaded = diffuseColor.xyz * (diffuse + specular);

    fragColor = vec4(texColorShaded, 1);
}