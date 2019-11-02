using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    [CustomEditor(typeof(Blocks.BlockGroup))]
    class BlockGroupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var group = target as Blocks.BlockGroup;

            foreach(var block in group.Blocks)
            {
                var texture = AssetPreview.GetAssetPreview(block.sprite);
                if (texture)
                    GUILayout.Label(texture);
            }
        }
    }
}
