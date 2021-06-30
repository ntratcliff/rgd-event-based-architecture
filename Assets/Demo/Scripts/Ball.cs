using UnityEngine;

namespace Demo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        private Rigidbody2D body;

        [SerializeField] private Vector2 initialVelocity = -Vector2.one;
        [SerializeField] private Vector2 axisMin = Vector2.one;
        private float speed;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            body.velocity = initialVelocity;
            speed = initialVelocity.magnitude;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // notify everyone that we collided with something
            EventDispatcher.Dispatch(new BallCollidedEvent(other));
        }

        private void FixedUpdate()
        {
            var v = body.velocity;
            
            // maintain speed
            v = v.normalized * speed;

            // make sure there is at least some movement on each axis
            v.x = InverseClamp(v.x, axisMin.x);
            v.y = InverseClamp(v.y, axisMin.y);

            body.velocity = v;
        }

        private float InverseClamp(float v, float min)
        {
            return Mathf.Abs(v) < min ? Mathf.Sign(v) * min : v;
        }
    }
}