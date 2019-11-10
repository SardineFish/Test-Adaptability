using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Project
{
    public static class InputUtility
    {
        static Dictionary<InputAction, bool> performedActions = new Dictionary<InputAction, bool>();
        public static void Init(IEnumerable<InputAction> inputActions)
        {
            foreach(var action in inputActions)
            {
                performedActions[action] = false;
                action.performed += Action_performed;
            }
        }

        private static void Action_performed(InputAction.CallbackContext ctx)
        {
            //UnityEngine.Debug.Log(ctx.action.name);
            performedActions[ctx.action] = true;
        }

        public static bool IsPressed(this InputAction inputAction)
        {
            return inputAction.ReadValue<float>() > .5f;
        }

        public static bool UsePressed(this InputAction inputAction)
        {
            var state = performedActions[inputAction];
            performedActions[inputAction] = false;
            return state;
        }
    }
    class InputState
    {
        public InputAction InputAction;
        public float Value;
    }
}
