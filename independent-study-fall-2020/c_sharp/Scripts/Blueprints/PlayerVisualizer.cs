namespace Indpendent_Study_Fall_2020.c_sharp.Scripts.Blueprints
{
    public class PlayerVisualizer
    {
        Wobble CreateAndAddEye(Vector3 position)
        {
            var eyeRotation = Quaternion.FromEulerAngles(0, 0,MathF.PI);
            var eye = new Wobble(.005f, 70f, position, SetupMaterials.EyeBall);
            eye.LocalScale = Vector3.One * .13f;
            eye.LocalRotation = eyeRotation;
                
            eye.Parent = _playerCamVisualizer;
            EntityManager.AddToWorldAndRenderer(eye);
            return eye;
        }
        
        WebCamVisualizer = EntityManager.AddToWorldAndRenderer(new Empty(SetupMaterials.EyeBall, SetupMaterials.ShadowMapSphere));
        
            WebCamVisualizer.LocalPosition = cameraPosition;
            WebCamVisualizer.LocalScale *= cameraScale;
            WebCamVisualizer.LocalRotation = Quaternion.FromEulerAngles(0,0,0); //negate table rolled
            WebCamVisualizer.AddCollider(new SphereCollider(WebCamVisualizer, true, 1f));

    }
}