using UnityEngine;
using System.Collections;

public class HealPowerUpController : PowerUpBaseController {

	
	public float healthToAdd;

	public override void performAction() {

		GameObject launchPad = GameObject.Find ("LaunchPad");
		launchPad.GetComponent<HealthController> ().addHealth (healthToAdd);

	}

}
