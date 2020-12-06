using System;
using CART_457.Attributes;
using CART_457.c_sharp.Renderer;
using CART_457.MaterialRelated;
using CART_457.Renderer;
using OpenTK.Graphics.ES11;
using Texture = FbxSharp.Texture;

namespace CART_457.Scripts.Setups
{
    public static class SetupMaterials
    {
        
        [IncludeInDrawLoop] public static Material BedroomCleanInCamera;
        [IncludeInDrawLoop] public static Material BedroomCeilingLampsInCamera;
        [IncludeInDrawLoop] public static Material BedroomDirtyOutCamera;
        
        [IncludeInDrawLoop] public static Material BedroomCleanCeilingLamps;
        [IncludeInDrawLoop] public static Material BedroomClean;
        
        [IncludeInDrawLoop] public static Material Basement;
        [IncludeInDrawLoop] public static Material DoorOpen;
        [IncludeInDrawLoop] public static Material DoorOpenHandle;
        [IncludeInDrawLoop] public static Material UberBag;
        


        [IncludeInDrawLoop] public static Material SolidSphereR1;
        
        
        [IncludeInDrawLoop] public static Material EyeBall;
        [IncludeInDrawLoop] public static Material ScreenR1;

        //dirt sphere room2
        
        [IncludeInDrawLoop] public static Material ShadowMapSphere;
        [IncludeInDrawLoop] public static Material ShadowMapPlane;
        [IncludeInDrawLoop] public static Material BedroomDirtyShadow;
        [IncludeInDrawLoop] public static Material BasementShadow;

        
        [IncludeInPostFX] public static Material PostProcessing;
        static SetupMaterials()
        {
            
            #region solid_color
            SolidSphereR1 = Material.EntitySolid(SetupFBOs.Room1, SetupMeshes.IcoSphereHighPoly, null);
            #endregion

            #region shadow maps
            ShadowMapSphere = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.IcoSphereHighPoly,
                null);
            
            ShadowMapPlane = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.Plane,
                null);
            
            BedroomDirtyShadow = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.BedroomDirty,
                null);
            
            BasementShadow = Material.EntityCastShadow(
                SetupFBOs.Shadow1,
                SetupMeshes.BedroomDirty,
                null);

            #endregion

            #region normal materials

            Action<Material> normaMaterialUniformSender = (mat) =>
            {
                UniformSender.SetFloat(mat, UniformSender.NORMAL_MAP_STRENGTH, 2);
                UniformSender.SetFloat(mat, UniformSender.SPECULAR_ROUGHNESS, 16);
                GL.Enable(EnableCap.CullFace);
            };
            Action<Material> normaMaterialUniformSenderNoCull = (mat) =>
            {
                UniformSender.SetFloat(mat, UniformSender.NORMAL_MAP_STRENGTH, 2);
                UniformSender.SetFloat(mat, UniformSender.SPECULAR_ROUGHNESS, 16);
                GL.Disable(EnableCap.CullFace);
            };
            
            #region normals shadow
            
            EyeBall = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Room1,
                SetupMeshes.Eyeball,
                SetupFBOs.Shadow1,
                SetupTextures.EyeDiffuse,
                SetupTextures.EyeNormal,
                SetupTextures.EyeSpecular,
                normaMaterialUniformSender
            );
            
            Basement = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Room1,
                SetupMeshes.Basement,
                SetupFBOs.Shadow1,
                SetupTextures.BasementDiffuse,
                SetupTextures.BasementNormal,
                SetupTextures.BasementSpecular,
                normaMaterialUniformSender
            );
            
            DoorOpen = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Room1,
                SetupMeshes.DoorOpen,
                SetupFBOs.Shadow1,
                SetupTextures.DoorOpenDiffuse,
                SetupTextures.DoorOpenNormal,
                SetupTextures.DoorOpenSpecular,
                normaMaterialUniformSender
            );
            
            DoorOpenHandle = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Room1,
                SetupMeshes.DoorOpenHandle,
                SetupFBOs.Shadow1,
                SetupTextures.DoorOpenHandleDiffuse,
                SetupTextures.DoorOpenHandleNormal,
                SetupTextures.DoorOpenHandleSpecular,
                normaMaterialUniformSender
            );
            
            UberBag = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Room1,
                SetupMeshes.UberBag,
                SetupFBOs.Shadow1,
                SetupTextures.UberBagDiffuse,
                SetupTextures.UberBagNormal,
                SetupTextures.UberBagSpecular,
                normaMaterialUniformSenderNoCull
            );
            
            BedroomClean = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Webcam,
                SetupMeshes.BedroomClean,
                SetupFBOs.Shadow1,
                SetupTextures.BedroomCleanDiffuse,
                SetupTextures.BedroomCleanNormal,
                SetupTextures.BedroomCleanSpecular,
                normaMaterialUniformSenderNoCull
            );
            
            BedroomCleanCeilingLamps = MaterialPreconfigs.NormalReceiveShadow(
                SetupFBOs.Webcam,
                SetupMeshes.BedroomCleanCeilingLamp,
                SetupFBOs.Shadow1,
                SetupTextures.BedroomCeilingLampsDiffuse,
                SetupTextures.BedroomCeilingLampsNormal,
                SetupTextures.BedroomCeilingLampsSpecular,
                normaMaterialUniformSenderNoCull
            );
            #endregion
            
            #region frustrum

            BedroomCleanInCamera = MaterialPreconfigs.NormalReceiveShadowFrustrum(
                SetupFBOs.Room1,
                SetupMeshes.BedroomClean,
                Globals.WebCam,
                SetupFBOs.Shadow1,
                SetupTextures.BedroomCleanDiffuse,
                SetupTextures.BedroomCleanNormal,
                SetupTextures.BedroomCleanSpecular,
                normaMaterialUniformSenderNoCull
            );
            
            BedroomCeilingLampsInCamera = MaterialPreconfigs.NormalReceiveShadowFrustrum(
                SetupFBOs.Room1,
                SetupMeshes.BedroomCleanCeilingLamp,
                Globals.WebCam,
                SetupFBOs.Shadow1,
                SetupTextures.BedroomCeilingLampsDiffuse,
                SetupTextures.BedroomCeilingLampsNormal,
                SetupTextures.BedroomCeilingLampsSpecular,
                normaMaterialUniformSenderNoCull
            );
            
            BedroomDirtyOutCamera = MaterialPreconfigs.NormalReceiveShadowFrustrum(
                SetupFBOs.Room1,
                SetupMeshes.BedroomDirty,
                Globals.WebCam,
                SetupFBOs.Shadow1,
                SetupTextures.BedroomDirtyDiffuse,
                SetupTextures.BedroomDirtyNormal,
                SetupTextures.BedroomDirtySpecular,
                normaMaterialUniformSenderNoCull
            );
            #endregion
            
            
            
            #endregion
            
            #region screen material
            ScreenR1 = Material.GenericEntityBased(SetupFBOs.Room1, SetupShaders.Screen, SetupMeshes.ViewSpaceQuad, null, 
                (entity, material) =>
                {
                    UniformSender.SendTransformMatrices(entity, material, material.RenderTarget.Camera);
                    UniformSender.SendGlobals(material);
                });
            ScreenR1.SetupSampler(UniformSender.MAIN_COLOR_FBO_SAMPLER, SetupFBOs.Webcam.ColorTexture1);
            ScreenR1.SetupSampler(UniformSender.SECONDARY_COLOR_FBO_SAMPLER, SetupFBOs.Webcam.ColorTexture2);

            #endregion
            
            #region postfx
            PostProcessing = Material.PostProcessing(SetupShaders.PostFX);
            PostProcessing.SetupSampler(UniformSender.MAIN_COLOR_FBO_SAMPLER, SetupFBOs.Room1.ColorTexture1);
            PostProcessing.SetupSampler(UniformSender.SECONDARY_COLOR_FBO_SAMPLER, SetupFBOs.Room1.ColorTexture2);
            // PostProcessing.SetupSampler(UniformSender.MAIN_DEPTH_FBO_SAMPLER, SetupFBOs.ScreenManager.DepthTexture);
            #endregion
            
        }



    }
}