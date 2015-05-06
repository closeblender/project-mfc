using UnityEngine;
using System.Collections;

public class BasicLinearMoveEnemyController : EnemyController
{

	public float speed;
	public float attackRange = .1f;
	public bool gravity = true;
	float lastAttack = 0;
	public float attackSpeed = .5f;
	public float attackDamage = 1;

	public override void Start ()
	{
		base.Start ();
		GetComponent<Rigidbody2D> ().gravityScale = gravity ? GetComponent<Rigidbody2D> ().gravityScale : 0;
	}

	public override void move ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (getDirectionToLaunchPad () * speed, GetComponent<Rigidbody2D> ().velocity.y);
	
	}

	public override bool inAttackRange ()
	{
		Debug.DrawRay (new Vector2 (transform.position.x, transform.position.y) + new Vector2 ((GetComponent<Collider2D> ().bounds.extents.x + .01f) * getDirectionToLaunchPad (), 0), new Vector2 (getDirectionToLaunchPad () * attackRange, 0));
		RaycastHit2D[] hits = Physics2D.RaycastAll (new Vector2 (transform.position.x, transform.position.y) + new Vector2 ((GetComponent<Collider2D> ().bounds.extents.x + .01f) * getDirectionToLaunchPad (), 0), new Vector2 (getDirectionToLaunchPad (), 0), attackRange);
		for (int i=0; i<hits.Length; i++) {
			if (hits [i].transform.tag.Equals ("LaunchPad")) {
				return true;
			}
		}
		return false;
	}
	
	public override void attack ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);

		// Should Attack
		if (Time.time > lastAttack + attackSpeed) {
			lastAttack = Time.time;

			// Attack
			launchPad.GetComponent<HealthController> ().doDamage (attackDamage);

		}

	}

	public override void die ()
	{
		base.die ();
		// TODO Add points?
		Destroy (gameObject);
	}

}
