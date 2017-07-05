using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour {

	private List<GameObject> tiles = new List<GameObject>();
	private List<GameObject> borderTiles = new List<GameObject>();
	private Vector2 topLeft;
	private Vector2 topRight;
	private Vector2 bottomLeft;
	private Vector2 bottomRight;
	private float tileWidth;
	private float tileHeight;

	private GameObject player;
	private GameObject camera;
	private Sprite[] textures;
	private int yCount;
	private int xCount;
	private GameObject simpleTilePrefab;

	private Vector2 mapMax;
	private Vector2 mapMin;
	
	void Start() {
		textures = Resources.LoadAll<Sprite>("Sprites/TileMaps/SimpleTiles");
		// var screenHeight = Screen.height;
		// var screenWidth = Screen.width;
		var screenHeight = 2*Camera.main.orthographicSize;
  		var screenWidth = screenHeight*Camera.main.aspect;

		simpleTilePrefab = Resources.Load("Prefabs/SimpleTiles") as GameObject;
		//RectTransform rt = (RectTransform)simpleTilePrefab.transform;
 
		var renderer = simpleTilePrefab.GetComponent<Renderer>();
		tileWidth = renderer.bounds.max.x - renderer.bounds.min.x;
		tileHeight = renderer.bounds.max.y - renderer.bounds.min.y;
		yCount = (int)(screenHeight / tileHeight) + 2;
		xCount = (int)(screenWidth / tileWidth) + 2;

		for (int i = 0; i < yCount * xCount * 2; i++) {
			var tile = Instantiate(simpleTilePrefab);
			tile.SetActive(false);
			tiles.Add(tile);
		}

		CalculateEdges();
		player = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("MainCamera");

		for (int row = 0; row < yCount; row ++) {
			for (int col = 0; col < xCount; col++) {
				
				simpleTilePrefab.GetComponent<SpriteRenderer>().sprite = GetRandomTexture();
				var tile = Instantiate(simpleTilePrefab, new Vector3((topLeft.x - tileWidth / 2) + tileWidth * col, (topLeft.y + tileHeight / 2) - tileHeight * row, 1), Quaternion.identity);
				tiles.Add(tile);
				if (row == 0 && col == xCount - 1) {
					mapMax = new Vector3((topLeft.x - tileWidth / 2) + tileWidth * col, (topLeft.y + tileHeight / 2) - tileHeight * row, 1);
				}
				if (row == yCount - 1 && col == 0) {
					mapMin = new Vector3((topLeft.x - tileWidth / 2) + tileWidth * col, (topLeft.y + tileHeight / 2) - tileHeight * row, 1);
				}
			}
		}
	}

	Sprite GetRandomTexture() {
		var rnd = (int)Random.Range(0, 10);
		Sprite txtr = null;
		if (rnd == 1) txtr = textures[3];
		else if (rnd == 2) txtr = textures[2];
		else txtr = textures[(int)Random.Range(0, 1)];
		return txtr;
	}

	void CalculateEdges() {
		topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
		topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		bottomRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
	}

	void PlaceTile(Vector2 newPostion) {
		for (int i = 0; i < tiles.Count; i++) {
			if (!tiles[i].activeInHierarchy) {
				tiles[i].transform.position = new Vector3(newPostion.x, newPostion.y, 1);
				tiles[i].transform.rotation = Quaternion.identity;
				tiles[i].GetComponent<SpriteRenderer>().sprite = GetRandomTexture();
				tiles[i].SetActive(true);
				break;
			}
		}
	}

	void Update() {
		CalculateEdges();
		// UP
		if (mapMax.y - tileHeight / 2 < topRight.y) {
			for (int col = 0; col < xCount + 1; col++) {
				var newPosition = new Vector2(mapMin.x + tileWidth * col, mapMax.y);
				PlaceTile(newPosition);
			}
			mapMax.y += tileHeight;
		}
		if (mapMax.y - tileHeight / 2 - tileHeight > topRight.y) {
			mapMax.y -= tileHeight;
		}
		// DOWN
		if (mapMin.y + tileHeight / 2 > bottomLeft.y) {
			for (int col = 0; col < xCount + 1; col++) {
				var newPosition = new Vector2(mapMin.x + tileWidth * col, mapMin.y);
				PlaceTile(newPosition);
			}
			mapMin.y -= tileHeight;
		}
		if (mapMin.y + tileHeight / 2 + tileHeight < bottomLeft.y) {
			mapMin.y += tileHeight;
		}
		// RIGHT
		if (mapMax.x - tileWidth / 2 < topRight.x) {
			for (int row = 0; row < yCount; row ++) {
				if(!(mapMax.y - tileHeight / 2 < topRight.y && row == 0)) {
					var newPosition = new Vector2(mapMax.x , mapMin.y + tileWidth * row + tileWidth);
					PlaceTile(newPosition);
				}
			}
			mapMax.x += tileWidth;
		}
		if (mapMax.x - tileHeight / 2 - tileWidth > topRight.x) {
			mapMax.x -= tileWidth;
		}
		// // LEFT
		if (mapMin.x + tileWidth / 2 > bottomLeft.x) {
			for (int row = 0; row < yCount; row ++) {
				var newPosition = new Vector2(mapMin.x , mapMin.y + tileWidth * row + tileWidth);
				PlaceTile(newPosition);
			}
			mapMin.x -= tileWidth;
		}
		if (mapMin.x + tileHeight / 2 + tileWidth < bottomLeft.x) {
			mapMin.x += tileWidth;
		}
		


		// if (player.transform.position.x < topLeft.x + tileWidth * 4 || player.transform.position.x > topRight.x - tileWidth * 4 ||
		// 	player.transform.position.y > topLeft.y - tileWidth * 4 || player.transform.position.y < bottomLeft.y + tileWidth * 4)
		// 	camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), Time.deltaTime * 2f);
		camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		
			
	}
}
