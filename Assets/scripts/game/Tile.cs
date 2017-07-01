using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	private Renderer renderer;
	private bool becameVisible = false;
	// Use this for initialization
	private Plane[] planes;
	void Start () {
		renderer = this.GetComponent<Renderer>();
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
	}

	void OnBecameInvisible() {
         becameVisible = true;
    }
	
	// Update is called once per frame
	void Update () {

		planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (!GeometryUtility.TestPlanesAABB(planes,renderer.bounds))
			this.gameObject.SetActive(false);
	}
}
