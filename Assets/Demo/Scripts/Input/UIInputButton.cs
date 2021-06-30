using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Demo.Input
{
    [RequireComponent(typeof(EventTrigger))]
    public class UIInputButton : MonoBehaviour
    {
        [Range(-1, 1)] [SerializeField]
        private Direction direction = Direction.Left;

        private void Awake()
        {
            var trigger = GetComponent<EventTrigger>();
            
            // helper method for adding listeners to the attached EventTrigger
            // getting anything other than onClick events from Unity buttons is a bit complicated
            // see: https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.EventTrigger.html
            void AddEntryListener(
                EventTriggerType eventID,
                UnityAction<BaseEventData> callback
            )
            {
                var entry = new EventTrigger.Entry {eventID = eventID};
                entry.callback.AddListener(callback);
                trigger.triggers.Add(entry);
            }

            // start moving on pointer down
            AddEntryListener(
                EventTriggerType.PointerDown,
                data => EventDispatcher.Dispatch(
                    new MoveEvent(MoveEvent.EventActionType.Start, direction)
                )
            );

            // stop moving on pointer up
            AddEntryListener(
                EventTriggerType.PointerUp,
                data => EventDispatcher.Dispatch(
                    new MoveEvent(MoveEvent.EventActionType.Stop, direction)
                )
            );
        }
    }
}