using System.Collections.Generic;
using CART_457.MaterialRelated;

namespace CART_457.Renderer
{
    public struct FBOBatch
    {
        public readonly FBO FBO;
        public List<MaterialBatch> MaterialBatches;

        public FBOBatch(FBO fbo)
        {
            FBO = fbo;
            MaterialBatches = new List<MaterialBatch>();
        }
    }
}