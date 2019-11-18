using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Test
{
    public class FrameRateDisplay : MonoBehaviour
    {
        public int TargetFPS = -1;
        // Use this for initialization
        void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = TargetFPS;
        }

        Queue<float> dts = new Queue<float>();
        // Update is called once per frame
        void Update()
        {
            if (dts.Count > 30)
                dts.Dequeue();
            dts.Enqueue(Time.deltaTime);
        }

        private void OnGUI()
        {
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(new Vector2(0, 0), new Vector2(1024, 1024)), (1 / (dts.Sum() / dts.Count)).ToString());
        }
    }
}
