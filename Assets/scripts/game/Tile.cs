using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	private Renderer renderer;
	private bool becameVisible = false;
	// Use this for initialization
	void Start () {
		renderer = this.GetComponent<Renderer>();
	}

	void OnBecameInvisible() {
         becameVisible = true;
     }
	
	// Update is called once per frame
	void Update () {
		if (!renderer.isVisible && becameVisible) {
			this.gameObject.SetActive(false);
		}
	}
}
