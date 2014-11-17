using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2_All_Hell_Breaks_Loose.Game
{
    public abstract class Observable
    {
        protected List<Observer> observers;

        public void registerObserver(Observer observer)
        {
            observers.Add(observer);
        }
        public void unregisterObservers()
        {
            observers.Clear();
        }

        public abstract void NotifyObservers();

    }
}
