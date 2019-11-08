using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PostProcessMaterialRenderer), PostProcessEvent.AfterStack, "Project/PostProcessWithMaterial")]
public class PostProcessWithMaterial : PostProcessEffectSettings
{
    [SerializeField]
    public Material mat;

    [SerializeField]
    public float T = 0;
}
public class PostProcessMaterialRenderer : PostProcessEffectRenderer<PostProcessWithMaterial>
{
    public override void Render(PostProcessRenderContext context)
    {
        context.command.Blit(context.source, context.destination, settings.mat);
    }
}