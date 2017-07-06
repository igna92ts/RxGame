using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public abstract class Consumable : Observer {

	// Use this for initialization
	public abstract void Activate();

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			this.Activate();
			this.gameObject.SetActive(false);
		}
	}
}
