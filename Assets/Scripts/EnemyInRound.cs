using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyInRound {
	public EnemySpawnerController.EnemyType enemyType;
	public int amount;
	public int spawnAmount;
	public int destroyedAmount;
}
