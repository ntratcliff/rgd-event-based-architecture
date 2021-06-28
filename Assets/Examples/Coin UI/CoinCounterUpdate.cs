using UnityEngine;
using UnityEngine.UI;

namespace Examples.Coin_UI.Update
{
    public class CoinCounterUI : MonoBehaviour
    {
        public Player Player;
        public Text Text;

        private void Update()
        {
            // called every frame 😔
            Text.text = Player.Coins.ToString();
        }
    }
}
