using CART_457.MaterialRelated;

namespace CART_457.EntitySystem.Scripts.EntityPrefabs
{
    public class Behaviorless : EntitySystem.Entity
    {
        public Behaviorless(Material [] mat)
        {
            SetupMaterials(mat);
        }
    }
}