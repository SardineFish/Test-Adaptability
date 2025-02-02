﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Event
{
    public interface IEventBehaviour
    {
        ReflectEventListener[] EventListeners { get; set; }
        EventBus EventTarget { get; set; }
    }
}