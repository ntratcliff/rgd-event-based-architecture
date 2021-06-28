namespace Examples.ClassicObserver
{
    public class Player : Subject
    {
        public float Health { get; private set; } = 100f;

        // ...

        void Hit(float damage)
        {
            // subtract damage from health
            Health -= damage;

            // notify observers
            Notify(new PlayerHurtEvent(damage));
        }
    }

    public class PlayerHurtEvent : IEvent
    {
        public readonly float Damage;

        public PlayerHurtEvent(float damage)
        {
            Damage = damage;
        }
    }
}
