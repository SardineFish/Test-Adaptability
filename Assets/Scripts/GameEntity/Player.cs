using UnityEngine;
using System.Collections;
using System;

namespace Project
{
    public class Player : GameEntity
    {
        public event Action OnPlayerDead;
        public void Kill()
        {
            OnPlayerDead?.Invoke();
        }
    }

}