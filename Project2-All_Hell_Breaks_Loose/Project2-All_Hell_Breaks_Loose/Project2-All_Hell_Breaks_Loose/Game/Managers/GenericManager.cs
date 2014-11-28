using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project2_All_Hell_Breaks_Loose.Game.Managers
{
    public class GenericManager<T> : Observable
    {
        protected List<T> objects;
        protected List<T> objectsToRemove;

        public GenericManager()
        {
            objects = new List<T>();
            objectsToRemove = new List<T>();
        }

        public void AddObject(T toAdd)
        {
            objects.Add(toAdd);
        }

        public void AddObjects(List<T> toAdd)
        {
            objects.AddRange(toAdd);
        }

        public void DeleteObject(T toDelete)
        {
            objectsToRemove.Add(toDelete);
        }

        public void DeleteObject(int index)
        {
            objectsToRemove.Add(objects[index]);
        }

        public void RemoveAtEndOfFrame()
        {
            foreach (T item in objectsToRemove)
            {
                objects.Remove(item);
            }
            objectsToRemove.Clear();
        }

        public int GetCount()
        {
            return objects.Count;
        }

        public T GetObject(int i)
        {
            return objects[i];
        }

        public override void NotifyObservers()
        {

        }

        


    }
}
