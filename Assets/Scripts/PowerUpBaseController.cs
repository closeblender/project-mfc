using UnityEngine;
using System.Collections;

public abstract class PowerUpBaseController : HealthController {
	
	float startTime;
	public float lifeTime = 2;
	
	void Start() {
		startTime = Time.time;
	}
	
	void Update() {
		if (Time.time - startTime > lifeTime) {
			Destroy(gameObject);
		}
	}
	
	public override void die () {
		performAction ();

		Destroy(gameObject);
	}
	
	public abstract void performAction();
	
}
