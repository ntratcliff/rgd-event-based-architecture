using System;
using System.Collections.Generic;

namespace Demo
{
    /// <summary>
    /// Manages global event notification and listener registration
    /// </summary>
    public static class EventDispatcher
    {
        /// <summary>
        /// All event callbacks keyed by event type
        /// </summary>
        private static Dictionary<Type, Action<IEvent>> delegates;

        /// <summary>
        /// Map of generic listeners to their typed listener
        /// </summary>
        private static Dictionary<Delegate, Action<IEvent>> listenerLookup;

        static EventDispatcher()
        {
            delegates = new Dictionary<Type, Action<IEvent>>();
            listenerLookup = new Dictionary<Delegate, Action<IEvent>>();
        }

        /// <summary>
        /// Registers a listener for an event
        /// </summary>
        /// <param name="callback">The listener method to be called</param>
        /// <typeparam name="T">The event type to listen to</typeparam>
        public static void AddListener<T>(Action<T> callback)
            where T : IEvent
        {
            var eventType = typeof(T);
            
            // callbacks stored as IEvent delegates, so we need to cast the
            // parameter to the event type (T) on invocation
            Action<IEvent> genericCallback = e => callback.Invoke((T) e);

            // store IEvent listener for removal later
            listenerLookup[callback] = genericCallback;
            
            // add listener callback to event delegate
            if (delegates.TryGetValue(eventType, out var del)
                && del != null)
            {
                delegates[eventType] += genericCallback;
            }
            else
            {
                delegates[eventType] = genericCallback;
            }
        }

        /// <summary>
        /// Removes the provided listener from the event
        /// </summary>
        /// <param name="callback">The listener method</param>
        /// <typeparam name="T">The event type</typeparam>
        public static void RemoveListener<T>(Action<T> callback)
            where T : IEvent
        {
            // find IEvent callback
            if (listenerLookup.TryGetValue(callback, out var listener))
            {
                // remove IEvent callback from event delegate
                Type eventType = typeof(T);
                delegates[eventType] -= listener;

                // remove event-typed callback from lookup dict
                listenerLookup.Remove(callback);
            }
            else
            {
                throw new Exception(
                    "Attempt to remove listener that hasn't been registered."
                );
            }
        }

        /// <summary>
        /// Notifies all listeners for an event
        /// </summary>
        /// <param name="e">The event object to send to listeners</param>
        public static void Dispatch(IEvent e)
        {
            if (delegates.TryGetValue(e.GetType(), out var del))
            {
                del.Invoke(e);
            }
        }
    }
}
