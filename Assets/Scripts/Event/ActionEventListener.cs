﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Event
{
    public class ActionEventListener<T> : EventListenerBase
    {
        public Action<T> Callback;
        public ActionEventListener(string eventName, Action<T> callback)
        {
            EventName = eventName;
            Callback = callback;
        }

        public override void Invoke(params object[] args)
        {
            if (args.Length <= 0)
                Callback?.Invoke(default(T));
            else
                Callback?.Invoke((T)args[0]);
        }
    }
}