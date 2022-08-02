using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.ObserverSystem
{
    /// <summary>
    /// Generic Observer class that calls attached methods when the value changes.
    /// </summary>
    /// <typeparam name="T">Data value held by observer</typeparam>
    public class Observable<T>
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                onValueChanged.CallListeners(Value);
            }
        }
        GameEvent<T> onValueChanged = new GameEvent<T>();
        /// <summary>
        /// Creates a new Observable data
        /// </summary>
        /// <param name="value">Initial value</param>
        public Observable(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Adds an observer, will not be called until value changes
        /// </summary>
        /// <param name="observer">Method called on value change</param>
        public void AddObserver(System.Action<T> observer)
        {
            onValueChanged.AddListener(observer);
        }
        /// <summary>
        /// Removes an observer, throws an error if observer does not exist
        /// </summary>
        /// <param name="observer">Reference to the action added previously</param>
        public void RemoveObserver(System.Action<T> observer)
        {
            onValueChanged.RemoveListener(observer);
        }
        /// <summary>
        /// Clears all observers
        /// </summary>
        public void RemoveObservers()
        {
            onValueChanged.RemoveListeners();
        }
        /// <summary>
        /// Manually runs all the methods attached with the current data value
        /// </summary>
        public void CallObservers()
        {
            onValueChanged.CallListeners(Value);
        }
    }
}
