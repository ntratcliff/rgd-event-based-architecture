using System;
using System.Collections.Generic;
using UnityEngine;
using Examples.EventSystem;
using UnityEngine.UI;

namespace Examples.Gotchas
{
    /// <summary>
    /// Keeps track of all registered listeners to be removed in OnDestroy
    /// </summary>
    public class CustomBehaviour : MonoBehaviour
    {
        private List<Action> removeListeners;

        /// <summary>
        /// Adds a listener and remembers to remove it later
        /// </summary>
        protected void AddListener<T>(Action<T> callback)
            where T : IEvent
        {
            // initialize list if necessary
            removeListeners ??= new List<Action>();

            // register listener
            EventDispatcher.AddListener(callback);

            // add anonymous remove action to list
            removeListeners.Add(() => EventDispatcher.RemoveListener(callback));
        }

        protected virtual void OnDestroy()
        {
            // automatically remove all listeners on destroy
            RemoveAllListeners();
        }

        private void RemoveAllListeners()
        {
            // invoke remove actions
            foreach (var remove in removeListeners)
            {
                remove.Invoke();
            }
            
            // clear list
            removeListeners.Clear();
        }
    }
    
    public class CoinCounterUI : CustomBehaviour
    {
        public Text Text;

        private void Awake()
        {
            AddListener<CoinCollectedEvent>(Coin_Collected);
        }

        private void Coin_Collected(CoinCollectedEvent e)
        {
            Text.text = e.Source.Coins.ToString();
        }
    }
}
