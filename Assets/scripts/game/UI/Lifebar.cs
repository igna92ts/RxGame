using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : Observer {

	[Observing("PlayerStore")] int playerLife;
	[Observing("PlayerStore")] int maxHearts;
	public GameObject heartPrefab;
	public List<Sprite> heartStages;
	private GameObject[] hearts = new GameObject[8];
	private int heartCount = 4;
	const int MAX_HEARTS = 8;
	private int previousLife = 0;
	void Start () {
		for (int i = 0; i < MAX_HEARTS; i++){
			var heart = Instantiate(heartPrefab);
			heart.GetComponent<Image>().sprite = heartStages[0];
			heart.transform.SetParent(this.transform);
			if(i > 3)
				heart.SetActive(false);
			hearts[i] = heart;
		}
	}

	void OnEnable() {
		for (int i = 0; i < 4; i++){
			if (hearts[i] != null)
				hearts[i].SetActive(true);
			heartCount = 4;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHeart();
	}

	void UpdateHeart() {
		var leftover = playerLife % 4;
		if(previousLife != playerLife) {
			var index = (int)(playerLife / 4);
			for (int i = 0; i < hearts.Length; i++){
				if(i >= index && leftover == 0) {
					hearts[i].SetActive(false);
					if(previousLife > playerLife + 1)
						PlayerStore.Instance.Set<int>("maxHearts", maxHearts - 1);
				}
			}
			if (heartCount < maxHearts && hearts.Length > heartCount) {
				hearts[heartCount].SetActive(true);
				heartCount = maxHearts;
				hearts[index - 1].GetComponent<Image>().sprite = heartStages[0];
			}
			GameObject currentHeart = null;
			if(heartCount == 8 && index == 8) {
				hearts[index - 1].GetComponent<Image>().sprite = heartStages[0];
				currentHeart = hearts[index - 1];
			} else {
				currentHeart = hearts[index];
			}
			
			currentHeart.GetComponent<Image>().sprite = heartStages[leftover];
			previousLife = playerLife;
		}
	}
}
