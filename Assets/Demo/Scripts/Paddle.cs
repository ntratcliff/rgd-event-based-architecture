using UnityEngine;

namespace Demo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private float moveForce = 3f;
        [SerializeField] private float maxSpeed = 8f;

        private Rigidbody2D body;

        private bool moving;
        private int moveDirection;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();

            // listen for move events
            EventDispatcher.AddListener<MoveEvent>(On_Move);
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener<MoveEvent>(On_Move);
        }

        private void FixedUpdate()
        {
            if (moving) UpdateMoving();

            // clamp velocity
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
        }

        private void UpdateMoving()
        {
            // clear velocity if we changed directions
            if (moveDirection + Mathf.Sign(body.velocity.x) == 0)
            {
                body.velocity = Vector2.zero;
            }

            // calculate move force for current direction
            var movement = Vector2.right * (moveDirection * moveForce);

            // apply movement to body
            body.AddForce(movement);
        }

        private void On_Move(MoveEvent e)
        {
            var direction = (int) e.Direction;
            switch (e.Action)
            {
                case MoveEvent.EventActionType.Start:
                    moving = true;
                    moveDirection = direction;
                    break;
                // only stop moving if event is for our current direction
                case MoveEvent.EventActionType.Stop
                    when direction == moveDirection:
                    moving = false;
                    break;
            }
        }
    }
}