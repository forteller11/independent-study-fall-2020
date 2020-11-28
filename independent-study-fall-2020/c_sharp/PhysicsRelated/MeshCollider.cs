using CART_457.EntitySystem;
using CART_457.Helpers;
using CART_457.MaterialRelated;
using OpenTK.Mathematics;

namespace CART_457.PhysicsRelated
{
    public class MeshCollider: Collider
    {
        public TriangleCollider[] Triangles;
        

            public MeshCollider(Entity entity, bool isTransformRelative, Mesh mesh) : base(entity, isTransformRelative)
        {
            float[] b = mesh.Positions.Buffer;
            Triangles = new TriangleCollider[mesh.Positions.VerticesCount/ModelImporter.POINTS_IN_TRIANGLE];
            int ii = 0;
            for (int i = 0; i < Triangles.Length; i++)
            {
                int p1I = mesh.Positions.Stride * 0 + ii;
                int p2I = mesh.Positions.Stride * 1 + ii;
                int p3I = mesh.Positions.Stride * 2 + ii;
                
                Vector3 p1 = new Vector3(b[p1I + 0], b[p1I + 1], b[p1I + 2]);
                Vector3 p2 = new Vector3(b[p2I + 0], b[p2I + 1], b[p2I + 2]);
                Vector3 p3 = new Vector3(b[p3I + 0], b[p3I + 1], b[p3I + 2]);
                
                Triangles[i] = new TriangleCollider(entity, isTransformRelative, p1,p2,p3);

                ii += mesh.Positions.Stride*3;
            }
        }
    }
}