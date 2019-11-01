using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Project.Blocks;
using UnityEngine.UIElements;

namespace Project.Editor
{
    [CustomEditor(typeof(Block), true)]
    [CanEditMultipleObjects]
    class BlockEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var block = target as Block;
            /*if(block.sprite)
            {
                var texture = AssetPreview.GetAssetPreview(block.sprite);
                if(texture)
                {
                    GUILayout.Label(texture);
                    EditorGUI.DrawTextureTransparent(GUILayoutUtility.GetRect(100, 100), texture, ScaleMode.ScaleToFit);
                }
            }*/
        }
        public override bool HasPreviewGUI()
        {
            return (target as Block).sprite;
        }
        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var block = target as Block;
            var texture = AssetPreview.GetAssetPreview(block.sprite);
            if(texture)
            {
                var mat = new Material(Shader.Find("Project/SpritePreview"));
                //r.width = 500;
                //r.height = 500;
                EditorGUI.DrawPreviewTexture(r, texture, mat, ScaleMode.ScaleToFit);
            }
            // EditorGUI.DrawPreviewTexture(r, AssetPreview.GetAssetPreview(block.sprite));
            
        }
    }
}
