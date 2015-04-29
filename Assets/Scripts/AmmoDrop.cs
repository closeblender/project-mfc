using UnityEngine;
using System.Collections;

public class AmmoDrop : PowerUpBaseController {
	
	public override void performAction() {
		
		GameObject player = GameObject.Find ("_PlayerController");
		PlayerController pc = player.GetComponent<PlayerController> ();
		pc.ammoGained();
	}
	
}
