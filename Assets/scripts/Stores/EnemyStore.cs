using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyStore : BaseStore<EnemyStore> {

	[Observable] public int maxEnemies = 5;
	[Observable] public int currentEnemies = 0;
}
