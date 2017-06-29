using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private List<object> observing = new List<object>();
    public static Dictionary<string, IEnumerable<FieldInfo>> publicObsFields = new Dictionary<string, IEnumerable<FieldInfo>>();
    public static Dictionary<string, IEnumerable<FieldInfo>> obsFields = new Dictionary<string, IEnumerable<FieldInfo>>();

    /// <summary>
    /// This function takes all fields marked with the `Observing` attribute and, based on the store parameter sent to it, it finds it a registers to that field
    /// inside that store using reflection.
    /// </summary>
    private void MapFieldsToStore() { 
        CheckPublicFields();     
        var fields = GetObsFields();
        foreach(var f in fields) {
            var customAttrs = f.GetCustomAttributes(false);
            foreach(var attrs in customAttrs) {
                var oattr = (Observing) attrs;
                var store = (ObservableStore)Type.GetType(oattr.store).BaseType.GetProperty("Instance").GetValue(null, null);
                store.Register(f.Name, this);
            }
        }
    }

    private IEnumerable<FieldInfo> GetObsFields() {
        IEnumerable<FieldInfo> fields = null;
        if(Observer.obsFields.ContainsKey(this.GetType().Name)) {
            fields = Observer.obsFields[this.GetType().Name];
        } else {
            fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(prop => prop.IsDefined(typeof(Observing), false));
            Observer.obsFields.Add(this.GetType().Name, fields);
        }
        return fields;
    }

    private void CheckPublicFields() {
        IEnumerable<FieldInfo> publicFields = null;
        if(Observer.publicObsFields.ContainsKey(this.GetType().Name)) {
            publicFields = Observer.publicObsFields[this.GetType().Name];
        } else {
            publicFields = this.GetType().GetFields().Where(prop => prop.IsDefined(typeof(Observing), false));
            Observer.publicObsFields.Add(this.GetType().Name, publicFields);
        }
        if(publicFields.ToArray().ToList().Count > 0)    
            throw new Exception("Observing fields on " + this.GetType() + " can only be private fields");
    }

    void Awake() {
        MapFieldsToStore();
    }

    // private Dictionary<object, object> syncUps = new Dictionary<object, object>();
    // private object temp;
    // public object Watch(object value) {
    //     temp = value;
    //     return value;
    // }

    // public void With(object o) {
    //     syncUps.Add(o, this);
    // }
}