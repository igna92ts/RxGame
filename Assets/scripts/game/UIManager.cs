using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public GameObject gameScreen;
	public GameObject splashScreen;
	public GameObject map;
	public GameObject controlls;
	public GameObject enemyManager;

	public GameObject menu;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame() {
		gameScreen.SetActive(true);
		enemyManager.SetActive(true);
		map.SetActive(true);
		splashScreen.SetActive(false);
	}

	public void Stop() {
		gameScreen.SetActive(false);
		enemyManager.SetActive(false);
		map.SetActive(false);
		splashScreen.SetActive(true);
	}

	public void RestartGame() {
		Stop();
		StartGame();
	}
	
	public void OpenMenu() {
		menu.SetActive(true);
	}
}
