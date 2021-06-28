using System;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.Coin_UI.Observer
{
    public class CoinCounterUI : MonoBehaviour
    {
        public Player Player;
        public Text Text;

        private void Awake()
        {
            Player.OnCoinCollected += Coin_Collected;
        }

        // only called when coin value changes
        private void Coin_Collected()
        {
            Text.text = Player.Coins.ToString();
        }
    }
}
