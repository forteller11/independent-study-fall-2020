using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public abstract class RenderBatch
    {
        public string RenderID;
        public string ParentRenderID;
        
        public RenderBatch Batch;
        public List<RenderBatch> ChildrenBatches = new List<RenderBatch>();
        public virtual void SetDrawStatesAndCallInnerLoops()
        {
            for (int i = 0; i < ChildrenBatches.Count; i++)
            {
                ChildrenBatches[i].SetDrawStatesAndCallInnerLoops();
            }
        }
    }
}