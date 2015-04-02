using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int weaponIndex = 0;
	public Weapon[] weapons;
	float lastFire;
	public Text weaponText;

	void Update () {
		weaponText.text = "Weapon: " + weapons [weaponIndex].name;

		if (Input.GetMouseButtonDown (0)) {
			fire ();
		}
		if (Input.GetMouseButton (0) && weapons[weaponIndex].fireType == Weapon.FireType.Auto) {
			// Held Down
			if(Time.time > lastFire + weapons[weaponIndex].fireRate) {
				fire();
			}
		}

		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			weaponIndex = 0;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha2)) {
			weaponIndex = 1;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha3)) {
			weaponIndex = 2;
		}
	}

	public void fire() {
		lastFire = Time.time;
		RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);
		for(int i=0;i<hits.Length;i++) {
			if(hits[i].collider.transform.GetComponent<HealthController>() != null && !hits[i].collider.transform.tag.Equals("LaunchPad")) {
				// Not a launch pad
				hits[i].collider.transform.GetComponent<HealthController>().doDamage(weapons[weaponIndex].damage);
				if(!weapons[weaponIndex].collateral) {
					return;
				}
			}
		}
	}

}
