using System.Collections.Generic;
using Indpendent_Study_Fall_2020.Helpers;
using Indpendent_Study_Fall_2020.MaterialRelated;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public class FBOBatch : IUniqueName
    {
        public readonly FBO FBO;
        public List<MaterialBatch> MatchBatches = new List<MaterialBatch>();
        
        public FBOBatch(FBO fbo) => FBO = fbo;
        public string GetUniqueName() => FBO.Name;

        public void SetState()
        {
            //todo bind FBO to rendering buffer
        }
    }
}