using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Event
{
    public abstract class EventListenerBase
    {
        public string EventName { get; set; }
        public bool Once = false;
        public abstract void Invoke(params object[] args);
    }
}
