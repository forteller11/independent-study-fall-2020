using System.Collections.Generic;

namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public abstract class RenderBatch
    {
        public RenderBatch Batch;
        public List<RenderBatch> ChildrenBatches;
        public string RenderID;
        public string ParentRenderID;
        public virtual void SetDrawStatesAndCallInnerLoops()
        {
            for (int i = 0; i < ChildrenBatches.Count; i++)
            {
                ChildrenBatches[i].SetDrawStatesAndCallInnerLoops();
            }
        }
    }
}