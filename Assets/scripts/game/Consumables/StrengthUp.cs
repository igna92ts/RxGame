using System;
using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public class StrengthUp : Consumable {
    [Observing("PlayerStore")] int strMulti;

    public override void Activate()
    {
       PlayerStore.Instance.Set<int>("strMulti", 2);
    }
}
