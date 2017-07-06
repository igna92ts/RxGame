using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

class PlayerStore : BaseStore<PlayerStore> {

	[Observable] public int playerLife = 100;
	[Observable] public int playerScore = 0;
	[Observable] public bool gameLost = false;
	[Observable] public bool pause = false;
}
