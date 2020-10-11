using System.Collections.Generic;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
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