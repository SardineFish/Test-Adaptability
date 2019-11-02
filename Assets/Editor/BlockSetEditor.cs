using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Project.Editor
{
    [CustomEditor(typeof(Blocks.BlockSet))]
    [CanEditMultipleObjects]
    class BlockSetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var set = target as Blocks.BlockSet;
            var group = serializedObject.FindProperty("m_Groups");
            var groups = set.BlockGroups
                .Where(obj => obj is Blocks.BlockGroup)
                .Select(obj => obj as Blocks.BlockGroup);
            for (var i = 0; i < group.arraySize; i++)
            {
                if (!(group.GetArrayElementAtIndex(i).objectReferenceValue is Blocks.IBlockGroup))
                {
                    group.DeleteArrayElementAtIndex(i);
                    continue;
                }
                var block = (group.GetArrayElementAtIndex(i).objectReferenceValue as Blocks.Block);
                if(groups.Any(g=>g.HasBlock(block)))
                {
                    group.DeleteArrayElementAtIndex(i);
                }
            }
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
