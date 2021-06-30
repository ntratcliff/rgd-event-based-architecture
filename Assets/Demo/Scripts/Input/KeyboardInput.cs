using UnityEngine;

namespace Demo.Input
{
    public class KeyboardInput : MonoBehaviour
    {
        [SerializeField] private KeyCode moveLeft = KeyCode.A;
        [SerializeField] private KeyCode moveRight = KeyCode.D;

        private void Update()
        {
            // check for input
            CheckKey(moveLeft, Direction.Left);
            CheckKey(moveRight, Direction.Right);
        }

        private static void CheckKey(KeyCode key, Direction direction)
        {
            if (UnityEngine.Input.GetKeyDown(key))
            {
                // start moving in direction on key down
                EventDispatcher.Dispatch(
                    new MoveEvent(MoveEvent.EventActionType.Start, direction)
                );
            }
            else if (UnityEngine.Input.GetKeyUp(key))
            {
                // stop moving in direction on key up
                EventDispatcher.Dispatch(
                    new MoveEvent(MoveEvent.EventActionType.Stop, direction)
                );
            }
        }
    }
}