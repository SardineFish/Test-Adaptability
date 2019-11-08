using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Project.FX
{
    [Serializable]
    [PostProcess(typeof(NumberFXRenderer), PostProcessEvent.BeforeStack, "Project/NumberFX", false)]
    public class NumberFX: PostProcessEffectSettings
    {
        public FloatParameter XScale = new FloatParameter() { value = 40 };
        public FloatParameter YScale = new FloatParameter() { value = .5f };
        public TextureParameter NumberTex = new TextureParameter() { };
    }
    public class NumberFXRenderer : PostProcessEffectRenderer<NumberFX>
    {
        Material visibilityMat;
        public override void Init()
        {
            visibilityMat = new Material(Shader.Find("Project/FX/VisibilityShader"));
            base.Init();
        }
        public override void Render(PostProcessRenderContext context)
        {
            if(!visibilityMat)
                visibilityMat = new Material(Shader.Find("Project/FX/VisibilityShader"));
            var sheet = context.propertySheets.Get(Shader.Find("Project/FX/NumberFX"));
            sheet.properties.SetFloat("_ScaleX", settings.XScale.value);
            sheet.properties.SetFloat("_ScaleY", settings.YScale.value);
            sheet.properties.SetTexture("_NumberTex", settings.NumberTex.value);
            var tex = Shader.PropertyToID("_VisibleAreaTex");
            context.command.GetTemporaryRT(tex, context.width, context.height, 0, FilterMode.Bilinear);
            context.command.SetRenderTarget(tex);
            context.command.ClearRenderTarget(true, true, Color.black);
            context.command.DrawRenderer(GameMap.BlocksMap.Instance.VisibilityLayer.GetComponent<UnityEngine.Tilemaps.TilemapRenderer>(), visibilityMat, 0, 0);
            context.command.SetRenderTarget(UnityEngine.Rendering.BuiltinRenderTextureType.CameraTarget);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
            context.command.ReleaseTemporaryRT(tex);
        }
    }

}
