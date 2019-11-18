using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.Input
{
    public class TouchInputManager : MonoBehaviour
    {
        [SerializeField]
        Joystick joystick;
        [SerializeField]
        GameObject button0;
        [SerializeField]
        GameObject button1;
        TouchInputDevice device;
        PointerEventData data;
        GraphicRaycaster raycaster;
        List<RaycastResult> results = new List<RaycastResult>(256);

        private void Awake()
        {
            device = InputSystem.AddDevice<TouchInputDevice>();
            device.OnInputUpdate += Device_OnInputUpdate;
            data = new PointerEventData(EventSystem.current);
            raycaster = GetComponentInParent<GraphicRaycaster>();
        }

        private TouchInputDeviceState Device_OnInputUpdate()
        {
            TouchInputDeviceState state = new TouchInputDeviceState();
            state.stick = joystick.Direction;
            if (Touchscreen.current != null)
            {
                for (var touchIdx = 0; touchIdx < Touchscreen.current.touches.Count; touchIdx++)
                {
                    var touch = Touchscreen.current.touches[touchIdx];
                    switch (touch.phase.ReadValue())
                    {
                        case UnityEngine.InputSystem.TouchPhase.Began:
                        case UnityEngine.InputSystem.TouchPhase.Moved:
                            goto AvailableTouch;
                    }
                    // Ingore this touch
                    continue;

                AvailableTouch:

                    //var touch = Touchscreen.current.activeTouches[touchIdx];
                    //Debug.Log($"{touch.touchId.ReadValue()} {touch.phase.ReadValue()} {touch.pressure.ReadValue()} {touch.radius.ReadValue()}");
                    //var pos = Touchscreen.current.activeTouches[touchIdx].position;
                    //Debug.Log(pos.ReadValue());
                    data.position = touch.position.ReadValue();//Input.GetTouch(touchIdx).position;

                    raycaster.Raycast(data, results);
                    //results.ForEach(result => Debug.Log(result.gameObject));
                    if (results.Count <= 0)
                        continue;
                    if (results[0].gameObject == button0)
                        state.button0 = 1;
                    if (results[0].gameObject == button1)
                        state.button1 = 1;

                    results.Clear();
                }
            }
            return state;
        }
    }
}
