using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public class UIManager : Observer {
	public GameObject gameScreen;
	public GameObject splashScreen;
	public GameObject map;
	public GameObject controlls;
	public GameObject enemyManager;
	public GameObject options;
	public GameObject gameOver;
	public GameObject lifeBar;

	public GameObject menu;

	[Observing("PlayerStore")] int playerLife;
	[Observing("PlayerStore")] bool gameLost;
	[Observing("PlayerStore")] bool pause;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerLife <= 0 && !gameLost) {
			PlayerStore.Instance.Set<bool>("gameLost", true);
			gameOver.SetActive(true);
		}
	}

	public void StartGame() {
		gameScreen.SetActive(true);
		enemyManager.SetActive(true);
		map.SetActive(true);
		splashScreen.SetActive(false);
		options.SetActive(true);
	}

	public void Stop() {
		gameScreen.SetActive(false);
		enemyManager.SetActive(false);
		map.SetActive(false);
		splashScreen.SetActive(true);
		PlayerStore.Instance.Set<bool>("gameLost", false);
		PlayerStore.Instance.Set<bool>("pause", false);
	}

	public void RestartGame() {
		Stop();
		StartGame();
	}
	
	public void OpenMenu() {
		menu.SetActive(true);
		PlayerStore.Instance.Set<bool>("pause", true);
	}
}
