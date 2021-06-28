using System.Collections.Generic;

namespace Examples.ClassicObserver
{
    public class Subject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        protected void Notify(IEvent e)
        {
            foreach (var observer in observers)
                observer.OnNotify(e);
        }
    }
}
