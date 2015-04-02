using UnityEngine;
using System.Collections;

public abstract class HealthController : MonoBehaviour
{

	public float maxHealth;
	public float health;

	public void doDamage (float damage)
	{
		health -= damage;
		if (health <= 0) {
			health = 0;
			die ();
		}
	}

	public abstract void die ();

}
