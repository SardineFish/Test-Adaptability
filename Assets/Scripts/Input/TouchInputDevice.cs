using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;

namespace Project.Input
{
    public struct TouchInputDeviceState : IInputStateTypeInfo
    {
        public FourCC format => new FourCC("TCHX");
        [InputControl(name = "button0", layout = "Button", bit = 0)]
        public byte button0;
        [InputControl(name = "button1", layout = "Button", bit = 0)]
        public byte button1;

        [InputControl(name = "stick0", layout = "Stick")]
        public Vector2 stick;
    }

#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    [InputControlLayout(displayName = "Touch Input Device", stateType = typeof(TouchInputDeviceState))]
    public class TouchInputDevice : InputDevice, IInputUpdateCallbackReceiver
    {
        public ButtonControl Button0 { get; private set; }
        public ButtonControl Button1 { get; private set; }
        public StickControl Stick0 { get; private set; }

        public event Func<TouchInputDeviceState> OnInputUpdate;


        static TouchInputDevice()
        {
            InputSystem.RegisterLayout<TouchInputDevice>();
        }

        public void OnUpdate()
        {
            if(OnInputUpdate != null)
            {
                var state = OnInputUpdate();
                InputSystem.QueueStateEvent(this, state);
            }
        }

        protected override void FinishSetup()
        {
            base.FinishSetup();
            Button0 = GetChildControl<ButtonControl>("button0");
            Button1 = GetChildControl<ButtonControl>("button1");
            Stick0 = GetChildControl<StickControl>("stick0");
        }
        [RuntimeInitializeOnLoadMethod]
        private static void InitializeInPlayer() { }
    }

}
