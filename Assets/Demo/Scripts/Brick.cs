using UnityEngine;
using UnityEngine.Events;

namespace Demo
{
    public class Brick : MonoBehaviour
    {
        public UnityEvent OnBroken;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.rigidbody.TryGetComponent(out Ball ball))
            {
                Break(ball);
            }
        }

        private void Break(Ball ball)
        {
            // notify everyone that we were broken
            // dispatched before calling Destroy to avoid nasty side-effects
            EventDispatcher.Dispatch(new BrickBrokenEvent(this, ball));
            OnBroken.Invoke();
            
            // destroy ourselves
            Destroy(gameObject);
        }
    }
}
