using Demo.Utilities.Pooling;
using UnityEngine;

namespace Demo.FX
{
    /// <summary>
    /// Plays a particle effect when a brick is broken
    /// </summary>
    [RequireComponent(typeof(Pool))]
    public class FXBrickExplode : MonoBehaviour
    {
        private Pool pool;

        private void Awake()
        {
            pool = GetComponent<Pool>();

            // add our listener
            EventDispatcher.AddListener<BrickBrokenEvent>(Brick_Broken);
        }

        // remove our listener when we're destroyed
        private void OnDestroy()
        {
            EventDispatcher.RemoveListener<BrickBrokenEvent>(Brick_Broken);
        }

        private void Brick_Broken(BrickBrokenEvent e)
        {
            // spawn a particle effect from the pool
            if (!pool.TryGet(out var go)) return;

            var fx = go.GetComponent<ParticleSystem>();
            var t = go.transform;

            // set position to brick position
            t.position = e.Source.transform.position;

            // set color to brick color
            var main = fx.main;
            main.startColor = e.Source.GetComponent<SpriteRenderer>().color;

            // play the effect
            fx.Play();
        }
    }
}