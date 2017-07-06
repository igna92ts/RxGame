using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rx {
    class BaseStore<T> : ObservableStore where T : class, new()
    {
        private static T instance;
        public static T Instance {
            get {
                if (instance == null) {
                    instance = new T();
                }
                return instance;
            }
            set {}
        }

        public BaseStore() {
            MapProps();
        }

        private void MapProps() {
            var fields = GetType().GetFields().Where(prop => prop.IsDefined(typeof(Observable), false));
            foreach(var f in fields) {
                state.Add(f.Name, f.GetValue(this));
                observers.Add(f.Name, new List<Observer>());
            }
        }

        // public A Get<A>(string key) {
        //     if(state.ContainsKey(key))
        //         return (A)state[key];
        //     else
        // 		throw new Exception("Missing key " + key + " in state of " +GetType()+" when trying to get a value");
        // }

        public void Set<A>(string key, A newValue) {
            if(state.ContainsKey(key)) {
                state[key] = newValue;
                NotifyObservers(key);
            } else {
                throw new Exception("Missing key " + key + " in state of " +GetType()+ " when trying to set a value");
            }
        }
    }
}