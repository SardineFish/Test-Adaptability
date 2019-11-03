using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.GameMap;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Mathematics;

namespace Project.GameMap.Editor
{
    public class UserComponentUIData
    {
        public GameMap.UserBlockComponent Component;
        public RenderTexture Texture;
        Mesh mesh;

        public UserComponentUIData(UserBlockComponent component)
        {
            mesh = new Mesh();
            Component = component;
            float2 renderSize = component.Bound.size.ToVector2() * 16;
            Texture = new RenderTexture(Mathf.FloorToInt(renderSize.x), Mathf.FloorToInt(renderSize.y), 0);
            Texture.format = RenderTextureFormat.ARGB32;
            Texture.filterMode = FilterMode.Point;
            Texture.Create();
            MeshBuilder mb = new MeshBuilder(Component.BlockType.sprite.vertices.Length * Component.Count);
            Vector2 offset = .5f * (component.Bound.size.ToVector2() - Vector2.one);
            foreach (var block in Component.Blocks)
            {
                mb.AddVerts(
                    Component.BlockType.sprite.vertices.Select(vert => (vert + block.Position + new Vector2(.5f,.5f)).ToVector3()).ToArray(),
                    Component.BlockType.sprite.triangles.Select(i => (int)i).ToArray(),
                    Component.BlockType.sprite.uv
                );
            }
            mesh = mb.ToMesh(mesh);
            Graphics.SetRenderTarget(Texture);
            var mat = new Material(Shader.Find("Project/ComponentTexture"));
            mat.SetTexture("_MainTex", Component.BlockType.sprite.texture);
            mat.SetPass(0);
            GL.PushMatrix();
            GL.LoadOrtho();
            Graphics.DrawMeshNow(mesh, Matrix4x4.TRS(math.float3(0,0,0), Quaternion.identity, new Vector3(1.0f / component.Bound.size.x, 1.0f / component.Bound.size.y, 1)));
            GL.PopMatrix();

        }

        ~UserComponentUIData()
        {
            Texture.Release();
            mesh.Clear();
            UnityEngine.Object.Destroy(mesh);
            UnityEngine.Object.Destroy(Texture);
        }
    }
}
