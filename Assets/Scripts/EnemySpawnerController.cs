using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour
{

	public static EnemySpawnerController instance;
	public Round[] rounds;
	public enum EnemyType {BasicGround = 0, BasicAir = 1};
	public Transform[] enemyPrefabs;
	public Transform[] spawnPoints;
	public int round = 0;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		StartCoroutine (spawnEnemies ());
	}

	IEnumerator spawnEnemies()
	{
		while (true) {

			if(rounds[round].allEnemiesSpawned()) {
				// Wait for everything to die
				while(!rounds[round].allEnemiesDied()) {
					yield return null;
				}
				round ++;
			} else {
				// Spawn an Enemy
				yield return new WaitForSeconds(Random.Range(rounds[round].minSpawnAmount,rounds[round].maxSpawnAmount));
				Vector3 spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)].position;
				Transform prefab = enemyPrefabs[(int)rounds[round].getSpawnEnemyType()];
				// Pick random vertical position
				spawnPoint.y = Random.Range(spawnPoint.y/2,spawnPoint.y);
				Instantiate(prefab, spawnPoint,Quaternion.identity);
			}

			yield return null;
		}

	}

	public void onEnemyDeath(EnemySpawnerController.EnemyType enemyType) {
		rounds [round].onEnemyDeath (enemyType);
	}

}
