using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour {

	private List<GameObject> tiles = new List<GameObject>();
	private Vector2 topLeft;
	private Vector2 topRight;
	private Vector2 bottomLeft;
	private Vector2 bottomRight;
	private float tileWidth;
	private float tileHeight;

	private GameObject player;
	private GameObject camera;
	
	void Start() {
		Sprite[] textures = Resources.LoadAll<Sprite>("Sprites/TileMaps/SimpleTiles");
		// var screenHeight = Screen.height;
		// var screenWidth = Screen.width;
		var screenHeight = 2*Camera.main.orthographicSize;
  		var screenWidth = screenHeight*Camera.main.aspect;

		var simpleTilePrefab = Resources.Load("Prefabs/SimpleTiles") as GameObject;
		//RectTransform rt = (RectTransform)simpleTilePrefab.transform;
 
		var renderer = simpleTilePrefab.GetComponent<Renderer>();
		tileWidth = renderer.bounds.max.x - renderer.bounds.min.x;
		tileHeight = renderer.bounds.max.y - renderer.bounds.min.y;
		var yCount = (int)(screenHeight / tileHeight) + 2;
		var xCount = (int)(screenWidth / tileWidth) + 2;

		for (int i = 0; i < yCount * xCount; i++) {
			var tile = Instantiate(simpleTilePrefab);
			tile.SetActive(false);
			tiles.Add(tile);
		}

		CalculateEdges();
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("MainCamera");

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

	void ReplaceTile(Vector2 newPostion) {
		for (int i = 0; i < tiles.Count; i++) {
			if (!tiles[i].activeInHierarchy) {
				tiles[i].transform.position = new Vector3(newPostion.x, newPostion.y, 1);
				tiles[i].transform.rotation = Quaternion.identity;
				tiles[i].SetActive(true);
			}
		}
	}

	void CalculateEdges() {
		topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
		topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		bottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
	}

	void Update() {
		CalculateEdges();

		// if (player.transform.position.x < topLeft.x + tileWidth * 4 || player.transform.position.x > topRight.x - tileWidth * 4 ||
		// 	player.transform.position.y > topLeft.y - tileWidth * 4 || player.transform.position.y < bottomLeft.y + tileWidth * 4)
		// 	camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), Time.deltaTime * 2f);
		camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		
			
	}
}
