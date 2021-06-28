using UnityEngine;

namespace Examples.Coin_UI.Update
{
    public class Player : MonoBehaviour
    {
        public int Coins { get; private set; } = 30;

        public void CollectCoin()
        {
            ++Coins;
        }
    }
}
