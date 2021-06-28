using System;
using UnityEngine;

namespace Examples.Coin_UI.Observer
{
    public class CoinCollectedFX : MonoBehaviour
    {
        public Player Player;
        public ParticleSystem FX;

        private void Awake()
        {
            Player.OnCoinCollected += Coin_Collected;
        }

        private void Coin_Collected()
        {
            FX.Play();
        }
    }
}
