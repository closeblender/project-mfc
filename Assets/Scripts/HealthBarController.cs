using UnityEngine;
using System.Collections;

public class HealthBarController : MonoBehaviour
{

	public HealthController health;
	public Transform healthBar;

	void Update ()
	{
		if (health.health == health.maxHealth) {
			// Hide
			GetComponent<SpriteRenderer> ().enabled = false;
			healthBar.GetComponent<SpriteRenderer> ().enabled = false;
		} else {
			// Show
			GetComponent<SpriteRenderer> ().enabled = true;
			healthBar.GetComponent<SpriteRenderer> ().enabled = true;

			// Health
			healthBar.localScale = new Vector3 (health.health / health.maxHealth, 1, 1);
		}
	}
	
	void Start ()
	{
		if (health == null) {
			health = transform.parent.GetComponent<HealthController> ();
		}
	}

}
