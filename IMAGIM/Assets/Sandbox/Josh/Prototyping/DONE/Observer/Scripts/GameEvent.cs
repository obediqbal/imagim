using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.ObserverSystem
{
    /// <summary>
    /// An event that holds one parameter that can be listened to and invoked.
    /// </summary>
    /// <typeparam name="T">Parameter of the event</typeparam>
    public class GameEvent<T>
    {
        List<System.Action<T>> listeners = new List<System.Action<T>>();

        /// <summary>
        /// Adds a listener to the event
        /// </summary>
        /// <param name="listener">Method to be attached</param>
        public void AddListener(System.Action<T> listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes a previously attached method, throws error if method does not exist
        /// </summary>
        /// <param name="listener">Method reference to be removed</param>
        public void RemoveListener(System.Action<T> listener)
        {
            listeners.Remove(listener);
        }
        /// <summary>
        /// Clears all attached method
        /// </summary>
        public void RemoveListeners()
        {
            listeners.Clear();
        }
        /// <summary>
        /// Calls all the method on this event with the parameter value
        /// </summary>
        /// <param name="value">Parameter value</param>
        public void CallListeners(T value)
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].Invoke(value);
            }
        }
    }
}
