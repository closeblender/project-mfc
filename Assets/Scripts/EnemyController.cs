using UnityEngine;
using System.Collections;

public abstract class EnemyController : HealthController
{

	public GameObject launchPad;
	public EnemySpawnerController.EnemyType enemyType;

	// Use this for initialization
	public virtual void Start ()
	{
		// Get launchPad
		launchPad = GameObject.FindGameObjectWithTag ("LaunchPad");

		// Ignore other enemeys
		Physics2D.IgnoreLayerCollision (8, 8);

	}

	// Runs every frame
	public virtual void Update ()
	{
		if (inAttackRange ()) {
			attack ();
		} else {
			move ();
		}
	}

	public int getDirectionToLaunchPad ()
	{
		return transform.position.x < launchPad.transform.position.x ? 1 : -1;
	}

	public abstract void move (); // Move the character towards the launchpad
	public abstract bool inAttackRange (); // Checks if the player can attack
	public abstract void attack (); // Attack

	public override void die() {
		EnemySpawnerController.instance.onEnemyDeath (enemyType);
	}

}
