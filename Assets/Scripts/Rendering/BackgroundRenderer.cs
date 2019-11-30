using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

namespace Project.Rendering
{
    public class BackgroundRenderer : ScriptableRendererFeature
    {
        [Serializable]
        public class Settings
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingSkybox;
        }
        public Settings settings;

        class CustomRenderPass : ScriptableRenderPass
        {
            Material bgMat;
            Mesh fullScreenMesh;
            int mainTexId;
            int spriteRectId;
            // This method is called before executing the render pass. 
            // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
            // When empty this render pass will render to the active camera render target.
            // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
            // The render pipeline will ensure target setup and clearing happens in an performance manner.
            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
            }

            // Here you can implement the rendering logic.
            // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
            // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
            // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                var cmd = CommandBufferPool.Get("Render Background");
                var transfrom = (renderingData.cameraData.camera.projectionMatrix * renderingData.cameraData.camera.worldToCameraMatrix).inverse;
                cmd.SetGlobalTexture(mainTexId, RenderingManager.Instance.BackgroundTile.texture);
                var rect = RenderingManager.Instance.BackgroundTile.rect;
                var textureRect = RenderingManager.Instance.BackgroundTile.textureRect;
                var sprite = RenderingManager.Instance.BackgroundTile;
                cmd.SetGlobalVector(spriteRectId, new Vector4(rect.x / sprite.texture.width, rect.y / sprite.texture.height, rect.width / sprite.texture.width, rect.height / sprite.texture.height));
                cmd.DrawMesh(fullScreenMesh, transfrom, bgMat, 0, 0);
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();
                CommandBufferPool.Release(cmd);
            }

            /// Cleanup any allocated resources that were created during the execution of this render pass.
            public override void FrameCleanup(CommandBuffer cmd)
            {
            }

            public void Setup(ref RenderingData renderingData)
            {
            }
            public void Init()
            {
                bgMat = new Material(Shader.Find("Project/BackgroundPass"));
                mainTexId = Shader.PropertyToID("_MainTex");
                spriteRectId = Shader.PropertyToID("_SpriteRect");
                fullScreenMesh = new Mesh();
                fullScreenMesh.Clear();
                fullScreenMesh.vertices = new Vector3[]
                {
                    new Vector3(-1,-1,0),
                    new Vector3(1,-1,0),
                    new Vector3(1,1,0),
                    new Vector3(-1,1,0),
                };
                fullScreenMesh.triangles = new int[]
                {
                    0, 1, 2,
                    2, 3, 0
                };
                fullScreenMesh.uv = new Vector2[]
                {
                    new Vector2(0, 0),
                    new Vector2(1, 0),
                    new Vector2(1, 1),
                    new Vector2(0, 1),
                };
            }
        }

        CustomRenderPass m_ScriptablePass;

        public override void Create()
        {
            m_ScriptablePass = new CustomRenderPass();
            m_ScriptablePass.Init();
            // Configures where the render pass should be injected.
            m_ScriptablePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }

        // Here you can inject one or multiple render passes in the renderer.
        // This method is called when setting up the renderer once per-camera.
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            m_ScriptablePass.renderPassEvent = settings.Event;
            m_ScriptablePass.Setup(ref renderingData);
            renderer.EnqueuePass(m_ScriptablePass);
        }
    }



}