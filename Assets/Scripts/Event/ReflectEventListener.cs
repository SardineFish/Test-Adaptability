using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Project.Event
{

    public class ReflectEventListener : EventListenerBase
    {
        public ReflectEventListener(string eventName, MethodInfo method, object @object)
        {
            EventName = eventName;
            Method = method;
            Object = @object;
        }
        public object Object { get; set; }
        public MethodInfo Method { get; set; }

        public override void Invoke(params object[] args)
        {
            Method.Invoke(Object, args);
        }
    }
}