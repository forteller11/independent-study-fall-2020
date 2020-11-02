using CART_457.MaterialRelated;

namespace CART_457.EntitySystem.Scripts.Blueprints
{
    public class Behaviorless : EntitySystem.Entity
    {
        public Behaviorless(Material [] mat)
        {
            AssignMaterials(mat);
        }
    }
}