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
            if(block.sprite)
            {
                GUILayout.Label(AssetPreview.GetAssetPreview(block.sprite));
                EditorGUI.DrawTextureTransparent(GUILayoutUtility.GetRect(100, 100), AssetPreview.GetAssetPreview(block.sprite), ScaleMode.ScaleToFit);
            }
        }
        public override bool HasPreviewGUI()
        {
            return (target as Block).sprite;
        }
        public override void OnPreviewGUI(Rect r, GUIStyle background)
        {
            var block = target as Block;
            // EditorGUI.DrawPreviewTexture(r, AssetPreview.GetAssetPreview(block.sprite));
            r.width = 500;
            r.height = 500;
            GUI.DrawTexture(r, AssetPreview.GetAssetPreview(block.sprite));
            
            base.OnPreviewGUI(r, background);
        }
    }
}
