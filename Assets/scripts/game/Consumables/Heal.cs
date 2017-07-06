using System;
using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public class Heal : Consumable {

	[Observing("PlayerStore")] int playerLife;

    public override void Activate()
    {
       PlayerStore.Instance.Set<int>("playerLife", playerLife + 10);
    }
}
