namespace Examples.UnityCSEvents.Unity
{
    public class PlayerHealthUI
    {
        public PlayerHealthUI(Player player)
        {
            // add listener for UnityEvent
            player.OnHurt.AddListener(Player_Hurt);

            player.OnHurt.AddListener(
                damage =>
                {
                    // do something ... 
                }
            );
        }

        public void Player_Hurt(float damage)
        {
            // update UI ...
        }
    }
}