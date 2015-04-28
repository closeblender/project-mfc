using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public MenuController mController;
	public int weaponIndex = 0;
	public Weapon[] weapons;
	float lastFire;
	public Text weaponText;
	public bool reloading = false;
	public Text tvReloading;
	public SpriteRenderer muzzleFlash;
	public float muzzleFlashLength;
	public float powerUpMultipler;
	public float powerUpMultiplerTill;

	void Start() {
		muzzleFlash.enabled = false;
	}

	void Update () {
		weaponText.text = "Weapon: " + weapons [weaponIndex].name + "; Ammo: " + weapons[weaponIndex].clipAmmo + "/" + weapons[weaponIndex].ammo;
		tvReloading.text = reloading ? "Reloading" : "";

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
		if (Input.GetKeyUp (KeyCode.Alpha1) && weapons[0].own) {
			weaponIndex = 0;
			reloading = false;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha2) && weapons[1].own) {
			weaponIndex = 1;
			reloading = false;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha3) && weapons[2].own) {
			weaponIndex = 2;
			reloading = false;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha4) && weapons[3].own) {
			weaponIndex = 3;
			reloading = false;
		}
		// Change Weapon
		if (Input.GetKeyUp (KeyCode.Alpha5) && weapons[4].own) {
			weaponIndex = 4;
			reloading = false;
		}
		// Reload
		if (Input.GetKeyUp (KeyCode.R)) {
			tryToReload();
		}
	}

	public void fire() {
		// Can you fire?
		if (weapons [weaponIndex].clipAmmo > 0 && !mController.open) {
			lastFire = Time.time;

			float doDamage = weapons [weaponIndex].damage * (powerUpMultiplerTill > Time.time ? powerUpMultipler : 1);

			weapons [weaponIndex].clipAmmo --;
			if(weapons [weaponIndex].damageType == Weapon.DamageType.Splash) {
				Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint (Input.mousePosition), weapons [weaponIndex].splashRadius);
				for (int i=0; i<hits.Length; i++) {
					if (hits [i].transform.GetComponent<HealthController> () != null && !hits [i].transform.tag.Equals ("LaunchPad")) {
						// Not a launch pad
						float distance = Vector2.Distance(hits [i].transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition));
						Debug.Log(distance);
						float t = 1 - (distance/weapons [weaponIndex].splashRadius);
						Debug.Log(t);
						float dam = doDamage * t;
						Debug.Log (dam);
						hits [i].transform.GetComponent<HealthController> ().doDamage (dam);
					}
				}

			} else {
				RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector3.zero);

				for (int i=0; i<hits.Length; i++) {
					if (hits [i].collider.transform.GetComponent<HealthController> () != null && !hits [i].collider.transform.tag.Equals ("LaunchPad")) {
						// Not a launch pad
						hits [i].collider.transform.GetComponent<HealthController> ().doDamage (doDamage);
						if (weapons [weaponIndex].damageType != Weapon.DamageType.Collateral) {
							break;
						}
						doDamage = doDamage * .5f;
					}
				}
			}

			if(weapons [weaponIndex].clipAmmo == 0) {
				tryToReload();
			}
			
			StopCoroutine("performMuzzleFlash");
			StartCoroutine("performMuzzleFlash",weaponIndex);
		} else {
			tryToReload();
		}
	}

	public void tryToReload() {
		if(!reloading && weapons[weaponIndex].ammo > 0 && weapons[weaponIndex].clipAmmo != weapons[weaponIndex].clipSize) {
			StartCoroutine(reloadGun(weaponIndex));
		}
	}

	IEnumerator reloadGun(int gun) {
		reloading = true;
		yield return new WaitForSeconds(weapons[gun].reloadTime);

		if (weaponIndex == gun && reloading) {
			// Did not change guns
			if(weapons[weaponIndex].ammo > weapons[weaponIndex].clipSize - weapons[weaponIndex].clipAmmo) {
				weapons[weaponIndex].ammo -= (weapons[weaponIndex].clipSize - weapons[weaponIndex].clipAmmo);
				weapons[weaponIndex].clipAmmo = weapons[weaponIndex].clipSize;
			} else {
				weapons[weaponIndex].clipAmmo += weapons[weaponIndex].ammo;
				weapons[weaponIndex].ammo = 0;
			}
		}

		reloading = false;

	}

	IEnumerator performMuzzleFlash(int gun) {
		muzzleFlash.enabled = true;
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		pos.z = 0;
		muzzleFlash.transform.position = pos;
		muzzleFlash.sprite = weapons [gun].muzzleFlash;
		yield return new WaitForSeconds (muzzleFlashLength);
		muzzleFlash.enabled = false;
	}

}
