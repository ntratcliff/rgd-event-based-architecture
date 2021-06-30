using UnityEngine;

namespace Demo.Input
{
    public class AIInput : MonoBehaviour
    {
        // note: there is some coupling in this case because this is just a rough example of "AI" input
        //      the paddle still doesn't care where the input is coming from, which is the important part

        [SerializeField] private Paddle paddle;
        [SerializeField] private Ball ball;

        private int currentDirection;
        private float lastUpdateTime = float.NegativeInfinity;

        private void Update()
        {
            // wait for update delay
            if (Time.time < lastUpdateTime) return;
            
            // compare position of ball and paddle
            var ballPos = ball.transform.position;
            var paddlePos = paddle.transform.position;
            var delta = ballPos.x - paddlePos.x;
            var direction = Mathf.RoundToInt(Mathf.Sign(delta));

            // start moving in the direction of the ball if we're not already
            if (currentDirection == direction) return;
            
            EventDispatcher.Dispatch(
                new MoveEvent(
                    MoveEvent.EventActionType.Start,
                    (Direction) direction
                )
            );
                
            currentDirection = direction;
        }
    }
}