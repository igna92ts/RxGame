using System;
using System.Collections.Generic;
using System.Reflection;

class ObservableStore
{
    protected Dictionary<string, List<Observer>> observers = new Dictionary<string, List<Observer>>();
    protected Dictionary<string, object> state = new Dictionary<string, object>();

    public void Register(string key, Observer observer) {
        if(observers.ContainsKey(key)) {
            observers[key].Add(observer);
            NotifyObservers(key);
        } else
            throw new Exception("There is no field " + key + " in " + GetType() + " store , change it in " + observer.GetType() + " to match one");
    }

    public void Unregister(string key, Observer observer) {
        observers[key].Remove(observer);
    }

    public void NotifyObservers(string key) {
        if(observers.ContainsKey(key)) {
            foreach(var observer in observers[key]) {
                observer.GetType().GetField(key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(observer, state[key]);
            }
        }
    }
}