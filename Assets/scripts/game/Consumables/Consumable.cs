using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Observer {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public abstract void Activate();

	// void OnTriggerEnter(Collider2D coll) {
	// 	if (coll.gameObject.tag == "Player") {
	// 		Activate();
	// 		this.gameObject.SetActive(false);
	// 	}
	// }
}
