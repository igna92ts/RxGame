﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerStore : BaseStore<PlayerStore> {

	[Observable] public int playerLife = 100;
}