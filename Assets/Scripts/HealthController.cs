using UnityEngine;
using System.Collections;

public abstract class HealthController : MonoBehaviour
{

	public float maxHealth;
	public float health;

	public void doDamage (float damage)
	{
		if (damage > 0) {
			health -= damage;
			if (health <= 0) {
				health = 0;
				die ();
			}
		}
	}

	public abstract void die ();

	public void addHealth(float add) {
		health += add;
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

}
