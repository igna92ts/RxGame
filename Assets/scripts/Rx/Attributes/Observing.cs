using System;
[AttributeUsage(AttributeTargets.Field)]
class Observing : Attribute
{
    public string store;
    public Observing(string store) {
        this.store = store;
    }
}