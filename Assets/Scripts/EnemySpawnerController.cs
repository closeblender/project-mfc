using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour
{

	public static EnemySpawnerController instance;
	public Round[] rounds;
	public enum EnemyType {BasicGround = 0, BasicAir = 1};
	public Transform[] enemyPrefabs;
	public Transform[] spawnPoints;
	public int round = 0;

	public Text tvRound;
	public Text tvCash;

	public int cash;
	public int roundStartCash;

	public MenuController menuController;

	public float powerUpChance = .1f;
	public Transform[] powerUpPrefab;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		StartCoroutine (spawnEnemies ());
	}

	void Update() 
	{
		tvRound.text = "Round: " + (round + 1);
		tvCash.text = "Cash: " + cash;
	}

	IEnumerator spawnEnemies()
	{
		while (true) {

			if(rounds[round].allEnemiesSpawned()) {
				// Wait for everything to die
				while(!rounds[round].allEnemiesDied()) {
					yield return null;
				}
				// show menu
				menuController.showEndRound(roundStartCash, cash, round);
				while(!menuController.startNextRound) {
					yield return null;
				}
				roundStartCash = cash;
				round ++;
			} else {
				// Spawn an Enemy
				yield return new WaitForSeconds(Random.Range(rounds[round].minSpawnAmount,rounds[round].maxSpawnAmount));
				Vector3 spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Length)].position;
				Transform prefab = enemyPrefabs[(int)rounds[round].getSpawnEnemyType()];
				// Pick random vertical position
				spawnPoint.y = Random.Range(spawnPoint.y/2,spawnPoint.y);
				if(spawnPoint.x > 0){
					Instantiate(prefab, spawnPoint,new Quaternion(0,180,0,0));
				}else{
					Instantiate(prefab, spawnPoint,Quaternion.identity);
				}
			}

			yield return null;
		}

	}

	public void onEnemyDeath(EnemySpawnerController.EnemyType enemyType, int reward) {
		rounds [round].onEnemyDeath (enemyType);
		cash += reward;
	}

	public void spawnPowerUp(Vector3 pos) {
		if (Random.value < powerUpChance) {
			Instantiate(powerUpPrefab[Random.Range(0,powerUpPrefab.Length)],pos,Quaternion.identity);
		}
	}

}
