using System;
using UnityEngine.Events;

namespace Examples.UnityCSEvents.CS
{
    public class Player
    {
        public float Health { get; private set; } = 100f;

        public event Action<float> OnHurt;

        // alternative way to declare C# event: 
        // public delegate void OnHurtDelegate(float damage);
        // public event OnHurtDelegate OnHurt;

        // ...

        void Hit(float damage)
        {
            // subtract damage from health
            Health -= damage;

            // notify observers
            OnHurt.Invoke(damage);
            
            OnHurt(damage); // same as above
        }
    }
}
