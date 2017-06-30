using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour {

	private List<GameObject> tiles = new List<GameObject>();
	void Start() {
		var screenHeight = Screen.height;
		var screenWidth = Screen.width;

		var simpleTilePrefab = Instantiate(Resources.Load("enemy")) as GameObject;
		RectTransform rt = (RectTransform)simpleTilePrefab.transform;
 
		float width = rt.rect.width;
		float height = rt.rect.height;
		var yCount = (screenHeight / height) + 2;
		var xCount = (screenWidth / width) + 2;

		Debug.Log(xCount);
		Debug.Log(yCount);
	}
}
