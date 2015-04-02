using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Round {
	public EnemyInRound[] enemies;
	public float minSpawnAmount;
	public float maxSpawnAmount;

	public EnemySpawnerController.EnemyType getSpawnEnemyType() {
		// First find types
		List<EnemySpawnerController.EnemyType> avalibleTypes = new List<EnemySpawnerController.EnemyType> ();
		for(int i=0;i<enemies.Length;i++) {
			if(enemies[i].spawnAmount < enemies[i].amount) {
				avalibleTypes.Add(enemies[i].enemyType);
			}
		}

		EnemySpawnerController.EnemyType enemyType = avalibleTypes[Random.Range (0,avalibleTypes.Count)];

		for(int i=0;i<enemies.Length;i++) {
			if(enemies[i].enemyType == enemyType) {
				enemies[i].spawnAmount ++;
			}
		}

		return enemyType;
	}

	public bool allEnemiesSpawned() {
		for(int i=0;i<enemies.Length;i++) {
			if(enemies[i].spawnAmount < enemies[i].amount) {
				return false;
			}
		}
		return true;
	}

	public bool allEnemiesDied() {
		for(int i=0;i<enemies.Length;i++) {
			if(enemies[i].destroyedAmount < enemies[i].amount) {
				return false;
			}
		}
		return true;
	}

	public void onEnemyDeath(EnemySpawnerController.EnemyType enemyType) {
		for(int i=0;i<enemies.Length;i++) {
			if(enemies[i].enemyType == enemyType) {
				// Add to destoryed
				enemies[i].destroyedAmount ++;
				return;
			}
		}
	}

}
