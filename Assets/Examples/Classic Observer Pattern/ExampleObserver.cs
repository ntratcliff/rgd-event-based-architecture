namespace Examples.ClassicObserver
{
    public class PlayerHealthUI : IObserver
    {
        public PlayerHealthUI(Player player)
        {
            player.AddObserver(this);
        }

        public void OnNotify(IEvent e)
        {
            var phe = (PlayerHurtEvent) e;

            // update UI ...
        }
    }
}
