using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : Observer {

	[Observing("PlayerStore")] int playerLife;
	public GameObject heartPrefab;
	public List<Sprite> heartStages;
	private GameObject[] hearts = new GameObject[8];
	const int MAX_HEARTS = 8;
	void Start () {
		for (int i = 0; i < MAX_HEARTS; i++){
			var heart = Instantiate(heartPrefab);
			heart.GetComponent<Image>().sprite = heartStages[0];
			heart.transform.parent = this.transform;
			if(i > 3)
				heart.SetActive(false);
			hearts[i] = heart;
		}
	}

	void OnEnable() {
		for (int i = 0; i < 4; i++){
			hearts[i].SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateHeart() {
		var leftover = playerLife % 4;
		var index = (int)(playerLife / 4);
		Debug.Log("gagfwf");
	}
}
