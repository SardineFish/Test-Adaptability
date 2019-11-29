using UnityEngine;
using System.Collections;
using System;

namespace Project
{
    public class Player : GameEntity
    {
        bool killed = false;
        public event Action OnPlayerDead;
        public void Kill()
        {
            if (!killed)
            {
                killed = true;
                OnPlayerDead?.Invoke();

            }

        }
    }

}