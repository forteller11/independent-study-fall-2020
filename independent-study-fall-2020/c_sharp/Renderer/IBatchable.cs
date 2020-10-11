namespace Indpendent_Study_Fall_2020.c_sharp.Renderer
{
    public interface IBatchable
    {
        string UniqueName { get; set; }
        string ParentNameInDrawingManager { get; set; }
    }
}