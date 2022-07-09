using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public class Observable<T>
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
                CallObservers();
            }
        }
        List<System.Action<T>> observers = new List<System.Action<T>>();
        public Observable(T value)
        {
            this.value = value;
        }

        public void AddObserver(System.Action<T> observer)
        {
            observers.Add(observer);
        }
        public void RemoveObserver(System.Action<T> observer)
        {
            observers.Remove(observer);
        }
        public void CallObservers()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].Invoke(Value);
            }
        }
    }
}
