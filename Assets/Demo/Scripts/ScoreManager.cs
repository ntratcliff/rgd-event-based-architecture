using System;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Manages player's score
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// Minimum combo value
        /// </summary>
        private const int comboBase = 1;

        [SerializeField] private int brickValue = 50;
        
        [NonSerialized] public int Score;

        private int combo = comboBase;

        private void Awake()
        {
            // listen for BrickBrokenEvents with the Brick_Broken method
            EventDispatcher.AddListener<BrickBrokenEvent>(Brick_Broken);
            EventDispatcher.AddListener<BallCollidedEvent>(Ball_Collided);
        }

        private void OnDestroy()
        {
            // stop listening for events when we're destroyed
            EventDispatcher.RemoveListener<BrickBrokenEvent>(Brick_Broken);
            EventDispatcher.RemoveListener<BallCollidedEvent>(Ball_Collided);
        }

        private void Brick_Broken(BrickBrokenEvent e)
        {
            // award points for breaking brick
            var award = brickValue * combo;
            Score += award;

            // notify points awarded
            EventDispatcher.Dispatch(
                new PointsAwardedEvent(award, Score, e.Source.transform)
            );

            // increment combo
            ++combo;

            // notify combo changed
            // alternatively, we could do this in the setter of a property
            EventDispatcher.Dispatch(new ComboChangedEvent(combo));
        }

        private void Ball_Collided(BallCollidedEvent e)
        {
            // reset combo when paddle is hit
            if (combo == comboBase 
                || !e.Collision.transform.CompareTag(Tags.Paddle)
            ) return;

            combo = comboBase;

            // notify combo changed
            EventDispatcher.Dispatch(new ComboChangedEvent(combo));
        }
    }
}