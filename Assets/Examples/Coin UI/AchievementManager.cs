using UnityEngine;

namespace Examples.Coin_UI.Observer
{
    public class AchievementManager : MonoBehaviour
    {
        public Player Player;
        private int coinsCollected;

        // ... 

        void Coin_Collected()
        {
            ++coinsCollected;
            if (coinsCollected >= 100)
            {
                // unlock achievement ...
            }
        }
    }
}
