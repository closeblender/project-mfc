using UnityEngine;
using System.Collections;

public abstract class HealthController : MonoBehaviour
{

	public float maxHealth;
	public float health;

	public AudioClip[] clips;
	public Transform soundPrefab;

	public void doDamage (float damage)
	{
		if (clips.Length > 0) {
			
			Transform sound = Instantiate(soundPrefab, Camera.main.transform.position, Quaternion.identity) as Transform;
			sound.GetComponent<AudioSource>().clip = clips[Random.Range(0,clips.Length)];
			sound.GetComponent<AudioSource>().Play();
		}

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
