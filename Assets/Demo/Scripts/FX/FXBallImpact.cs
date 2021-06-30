using Demo.Utilities.Pooling;
using UnityEngine;

namespace Demo.FX
{
    /// <summary>
    /// Plays a particle effect for ball impacts
    /// </summary>
    [RequireComponent(typeof(Pool))]
    public class FXBallImpact : MonoBehaviour
    {
        private Pool pool;

        private void Awake()
        {
            pool = GetComponent<Pool>();
            
            // add our listener
            EventDispatcher.AddListener<BallCollidedEvent>(Ball_Collided);
        }

        // remove our listener when we're destroyed
        private void OnDestroy()
        {
            EventDispatcher.RemoveListener<BallCollidedEvent>(Ball_Collided);
        }

        private void Ball_Collided(BallCollidedEvent e)
        {
            // spawn a particle effect from the pool
            if (!pool.TryGet(out var go)) return;
            
            var collision = e.Collision;
            var fx = go.GetComponent<ParticleSystem>();
            var t = go.transform;
            var contact = collision.GetContact(0);
                
            // set position/rotation to contact point/normal
            t.position = contact.point;
            t.right = contact.normal;
            
            // play the effect
            fx.Play();
        }
    }
}