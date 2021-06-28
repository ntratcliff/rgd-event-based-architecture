using System;
using UnityEngine;

namespace Examples.Coin_UI.Observer
{
    public class Player : MonoBehaviour
    {
        public int Coins { get; private set; } = 30;

        public event Action OnCoinCollected;

        public void CollectCoin()
        {
            ++Coins;
            OnCoinCollected.Invoke();
        }
    }
}
