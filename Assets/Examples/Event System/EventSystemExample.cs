using System;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.EventSystem
{
    /// <summary>
    /// The coin collected event
    /// </summary>
    public class CoinCollectedEvent : IEvent
    {
        public readonly Player Source;

        public CoinCollectedEvent(Player source)
        {
            Source = source;
        }
    }

    /// <summary>
    /// Player dispatches a CoinCollectedEvent when a coin is collected
    /// </summary>
    public class Player
    {
        public int Coins { get; private set; } = 30;

        public void CollectCoin()
        {
            var oldValue = Coins;
            ++Coins;
            EventDispatcher.Dispatch(new CoinCollectedEvent(this));
        }
    }
    
    public class CoinCounterUI : MonoBehaviour
    {
        public Text Text;

        private void Awake()
        {
            EventDispatcher.AddListener<CoinCollectedEvent>(Coin_Collected);
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener<CoinCollectedEvent>(Coin_Collected);
        }

        // only called when coin value changes
        private void Coin_Collected(CoinCollectedEvent e)
        {
            Text.text = e.Source.Coins.ToString();
        }
    }
}
