using System;
using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public class LifeUp : Consumable {
    [Observing("PlayerStore")] int maxHearts;
	[Observing("PlayerStore")] int playerLife;

    public override void Activate()
    {
	    if( maxHearts < 8) {
			PlayerStore.Instance.Set<int>("maxHearts", maxHearts + 1);
	    }
		if(playerLife + 4 <= 8 * 4)
			PlayerStore.Instance.Set<int>("playerLife", playerLife + 4);
    }
}
