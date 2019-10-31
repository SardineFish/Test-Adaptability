using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
    [InitializeOnLoad]
    public class StaticEditor
    {
        static StaticEditor()
        {
            AssemblyReloadEvents.afterAssemblyReload += () =>
            {
                SceneView.GetAllSceneCameras()
                    .ForEach(camera => camera.backgroundColor = new Color(.88f, .88f, .88f));
            };
        }
    }
}
