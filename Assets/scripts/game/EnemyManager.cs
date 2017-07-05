using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Observer {
	[Observing("EnemyStore")] int maxEnemies;
	[Observing("EnemyStore")] int currentEnemies;
	private List<GameObject> enemies = new List<GameObject>();
	public GameObject player;
	// Use this for initialization
	void Start () {
		// if(player == null)
		GameObject.Instantiate(player);
		var goo = Resources.Load("Prefabs/Enemies/Goo") as GameObject;
		for (int i = 0; i < 100; i++) {
			var enemy = Instantiate(goo);
			enemy.SetActive(false);
			enemies.Add(enemy);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (maxEnemies > currentEnemies) {
			for (int i = 0; i < enemies.Count; i++) {
				if (!enemies[i].activeInHierarchy && maxEnemies > currentEnemies) {
					var range = Random.Range(2f + 1 * currentEnemies, -(2f + 1 * currentEnemies));
					var rangeX = range > 0 ? range + 7 : range - 7;
					range = Random.Range(5f + 1 * currentEnemies, -(5f + 1 * currentEnemies));
					var rangeY = range > 0 ? range + 7 : range - 7;
					enemies[i].transform.position = new Vector3(player.transform.position.x + rangeX, player.transform.position.y + rangeY, 1);
					enemies[i].transform.rotation = Quaternion.identity;
					enemies[i].SetActive(true);
					
					
					var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
					if (GeometryUtility.TestPlanesAABB(planes,enemies[i].GetComponent<SpriteRenderer>().bounds))
						enemies[i].SetActive(false);
					else
						EnemyStore.Instance.Set<int>("currentEnemies", currentEnemies + 1);
					// break;
				}
			}
		}
	}
}
