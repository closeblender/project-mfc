using UnityEngine;
using System.Collections;

public class DamagePowerUpController  : PowerUpBaseController {
	
	
	public float damageMultipler;
	public float timeLength;
	
	public override void performAction() {

		GameObject player = GameObject.Find ("_PlayerController");
		PlayerController pc = player.GetComponent<PlayerController> ();
		pc.powerUpMultipler = damageMultipler;
		pc.powerUpMultiplerTill = Time.time + timeLength;
		
	}
	
}
