using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.FX
{
    [Serializable]
    [PostProcess(typeof(EditModeSceneBoundaryRenderer), PostProcessEvent.BeforeStack, "Project/EditModeSceneBoundary", false)]
    public class EditModeSceneBoundary : PostProcessEffectSettings
    {
    }
    public class EditModeSceneBoundaryRenderer : PostProcessEffectRenderer<EditModeSceneBoundary>
    {
        Material maskMat;
        Material mat;
        public override void Init()
        {
            base.Init();
            maskMat = new Material(Shader.Find("Project/Misc/RendererMask"));
            mat = new Material(Shader.Find("Project/PostProcess/SceneBoundary"));
        }
        public override void Render(PostProcessRenderContext context)
        {
            if (ScenesManager.Instance?.CurrentScene is null)
            {
                context.command.Blit(context.source, context.destination);
                return;
            }
            var scene = ScenesManager.Instance.CurrentScene;
            var cmd = context.command;
            var rt = Shader.PropertyToID("_SceneBoundaryMask");
            cmd.GetTemporaryRT(rt, context.width, context.height, 0, FilterMode.Point, RenderTextureFormat.ARGB32);
            cmd.SetRenderTarget(rt);
            cmd.ClearRenderTarget(false, true, Color.black);
            cmd.DrawRenderer(scene.BoundaryRenderer, maskMat);

            var sheet = context.propertySheets.Get(Shader.Find("Project/PostProcess/SceneBoundary"));
            cmd.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

            cmd.ReleaseTemporaryRT(rt);
            cmd.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);

        }
    }

}
