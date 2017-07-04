using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Observer {

	[Observing("PlayerStore")] int playerLife;
	[Observing("PlayerStore")] int playerScore;

	public Text life;
	public Text score;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		life.text = "Life: " + playerLife.ToString();
		score.text = "Score: " + playerScore.ToString();
	}
}
