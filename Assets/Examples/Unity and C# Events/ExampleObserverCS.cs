namespace Examples.UnityCSEvents.CS
{
    public class PlayerHealthUI
    {
        public PlayerHealthUI(Player player)
        {
            // add listener for C# event
            player.OnHurt += Player_Hurt;

            // you can also use anonymous functions:
            player.OnHurt += damage =>
            {
                // do something ...
            };
        }

        public void Player_Hurt(float damage)
        {
            // update UI ...
        }
    }
}
