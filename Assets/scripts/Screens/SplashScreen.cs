using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	public GameObject gameScreen;
	public GameObject map;
	public GameObject controlls;
	public GameObject enemyManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		gameScreen.SetActive(true);
		controlls.SetActive(true);
		enemyManager.SetActive(true);
		map.SetActive(true);
		this.gameObject.SetActive(false);
	}
}
