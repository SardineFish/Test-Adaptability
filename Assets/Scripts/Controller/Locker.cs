using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Project.Controller
{
    public class Locker
    {

        private Stack<Guid> Locks = new Stack<Guid>();
        public bool Locked => Locks.Count > 0;
        public Guid Lock()
        {
            var id = Guid.NewGuid();
            Locks.Push(id);
            return id;
        }
        public bool UnLock(Guid id)
        {
            if (!Locks.Contains(id))
                return true;
            if (Locks.Peek() != id)
                return false;
            Locks.Pop();
            return true;
        }
        public void UnLockAll()
        {
            Locks.Clear();
        }
    }

}