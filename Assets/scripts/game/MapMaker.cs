using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour {

	private List<GameObject> tiles = new List<GameObject>();
	private Vector2 topLeft;
	private Vector2 topRight;
	private Vector2 bottomLeft;
	private Vector2 bottomRight;


	
	void Start() {
		Sprite[] textures = Resources.LoadAll<Sprite>("Sprites/TileMaps/SimpleTiles");
		// var screenHeight = Screen.height;
		// var screenWidth = Screen.width;
		var screenHeight = 2*Camera.main.orthographicSize;
  		var screenWidth = screenHeight*Camera.main.aspect;

		var simpleTilePrefab = Resources.Load("Prefabs/SimpleTiles") as GameObject;
		//RectTransform rt = (RectTransform)simpleTilePrefab.transform;
 
		var renderer = simpleTilePrefab.GetComponent<Renderer>();
		float tileWidth = renderer.bounds.max.x - renderer.bounds.min.x;
		float tileHeight = renderer.bounds.max.y - renderer.bounds.min.y;
		var yCount = (int)(screenHeight / tileHeight) + 2;
		var xCount = (int)(screenWidth / tileWidth) + 2;

		topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
		topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		bottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

		for (int row = 0; row < yCount; row ++) {
			for (int col = 0; col < xCount; col++) {
				var rnd = (int)Random.Range(0, 10);
				Sprite txtr = null;
				if (rnd == 1) txtr = textures[3];
				else if (rnd == 2) txtr = textures[2];
				else txtr = textures[(int)Random.Range(0, 1)];
				simpleTilePrefab.GetComponent<SpriteRenderer>().sprite = txtr;
				Instantiate(simpleTilePrefab, new Vector3((topLeft.x - tileWidth / 2) + tileWidth * col, (topLeft.y + tileHeight / 2) - tileHeight * row, 1), Quaternion.identity);
			}
		}
	}

	void something() {
		Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
 
		if(pos.x < 0.0) Debug.Log("I am left of the camera's view.");
		if(1.0 < pos.x) Debug.Log("I am right of the camera's view.");
		if(pos.y < 0.0) Debug.Log("I am below the camera's view.");
		if(1.0 < pos.y) Debug.Log("I am above the camera's view.");
	}
}
