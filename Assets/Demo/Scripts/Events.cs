using UnityEngine;

namespace Demo
{
    /// <summary>
    /// Tells the paddle to move
    /// </summary>
    public class MoveEvent : IEvent
    {
        public readonly Direction Direction;
        public readonly EventActionType Action;

        public MoveEvent(EventActionType action, Direction direction)
        {
            Action = action;
            Direction = direction;
        }

        public enum EventActionType
        {
            Start,
            Stop
        }
    }

    /// <summary>
    /// Notifies that a brick has been broken
    /// </summary>
    public class BrickBrokenEvent : IEvent
    {
        public readonly Brick Source;
        public readonly Ball Ball;

        public BrickBrokenEvent(Brick source, Ball ball)
        {
            Source = source;
            Ball = ball;
        }
    }

    /// <summary>
    /// Sent whenever a ball collides with something
    /// </summary>
    public class BallCollidedEvent : IEvent
    {
        public readonly Collision2D Collision;

        public BallCollidedEvent(Collision2D collision)
        {
            Collision = collision;
        }
    }

    /// <summary>
    /// Sent whenever points are awarded to the player
    /// </summary>
    public class PointsAwardedEvent : IEvent
    {
        public readonly int Award;
        public readonly int Score;
        public readonly Transform Source;

        public PointsAwardedEvent(
            int award, int score, Transform source
        )
        {
            Award = award;
            Score = score;
            Source = source;
        }
    }

    /// <summary>
    /// Sent whenever the player's combo changes
    /// </summary>
    public class ComboChangedEvent : IEvent
    {
        public readonly int Value;

        public ComboChangedEvent(int value)
        {
            Value = value;
        }
    }
}