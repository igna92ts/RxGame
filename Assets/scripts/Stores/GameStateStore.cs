using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

class GameStateStore : BaseStore<GameStateStore> {

	[Observable] public bool paused = false;
}
