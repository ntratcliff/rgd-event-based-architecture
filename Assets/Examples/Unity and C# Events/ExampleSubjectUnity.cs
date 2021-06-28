using System;
using UnityEngine.Events;

namespace Examples.UnityCSEvents.Unity
{
    public class Player
    {
        public float Health { get; private set; } = 100f;

        // UnityEvents with parameters must be implemented as a
        // Serializable class to appear in the inspector
        [Serializable]
        public class PlayerHurtUnityEvent : UnityEvent<float>
        {
        }

        public PlayerHurtUnityEvent OnHurt;

        // ...

        void Hit(float damage)
        {
            // subtract damage from health
            Health -= damage;

            // notify observers
            OnHurt.Invoke(damage);
        }
    }
}
