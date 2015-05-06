using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public PlayerController pController;
	public HealthController launchPad;
	public EnemySpawnerController eController;
	public bool startNextRound = false;

	public GameObject endOfRoundMenu;
	public Text tvEndOfRound;
	public Text tvStartCash;
	public Text tvRoundCash;
	public Text tvTotalCash;
	
	public GameObject storeMenu;
	public Text[] tvWeapons;
	public Image[] ivWeapons;
	public Vector2[] heal;
	public Text[] tvHeal;
	public Image[] ivHeal;
	public Vector2[] fortify;
	public Text[] tvFortify;
	public Image[] ivFortify;
	public int maxLaunchpadHealth = 500;
	public bool open = false;

	
	public GameObject youLosePanel;
	public Text tvroundLost;

	void Start() {
		endOfRoundMenu.SetActive (false);
		storeMenu.SetActive (false);
	}

	void Update () {
	
	}


	public void showEndRound(int startCash, int cash, int round) {
		startNextRound = false;
		endOfRoundMenu.SetActive (true);
		open = true;

		// Set titles
		tvEndOfRound.text = "End Of Round " + (round + 1);
		tvStartCash.text = "Start Cash: " + startCash;
		tvRoundCash.text = "Round Cash: " + (cash - startCash);
		tvTotalCash.text = "Total Cash: " + cash;

	}

	public void openStore() {
		endOfRoundMenu.SetActive (false);
		storeMenu.SetActive (true);

		// Set Gun Buttons
		for (int i=0; i<tvWeapons.Length; i++) {
			if(pController.weapons[i].own) {
				tvWeapons[i].text = "Buy " + pController.weapons[i].buyAmmoAmount + " " + pController.weapons[i].name + " Ammo For $" + pController.weapons[i].buyAmmoPrice;
				ivWeapons[i].color = eController.cash >= pController.weapons[i].buyAmmoPrice ? Color.white : Color.red;
			} else {
				tvWeapons[i].text = "Buy " + pController.weapons[i].name + " For $" + pController.weapons[i].buyPrice;
				ivWeapons[i].color = eController.cash >= pController.weapons[i].buyPrice ? Color.white : Color.red;
			}
		}

		// Set Heal Buttons
		for (int i=0; i<tvHeal.Length; i++) {
			tvHeal[i].text = "Heal +" + (int)heal[i].x + " For $" + (int)heal[i].y;
			ivHeal[i].color = eController.cash >= (int)heal[i].y && (launchPad.maxHealth - launchPad.health) >= (int)heal[i].x ? Color.white : Color.red;
		}

		// Set Fortify Buttons
		for (int i=0; i<tvFortify.Length; i++) {
			tvFortify[i].text = "Fortify Launchpad +" + (int)fortify[i].x + " For $" + (int)fortify[i].y;
			ivFortify[i].color = eController.cash >= (int)fortify[i].y && (launchPad.maxHealth - (int)fortify[i].x) <= maxLaunchpadHealth ? Color.white : Color.red;
		}

	}

	public void nextRound() {
		endOfRoundMenu.SetActive (false);
		storeMenu.SetActive (false);
		startNextRound = true;
		open = false;
	}

	public void onGunButton(int index) {
		if(pController.weapons[index].own) {
			// Try to buy ammo
			if(pController.weapons[index].ammo < pController.weapons[index].maxAmmo){
				if(eController.cash >= pController.weapons[index].buyAmmoPrice) {
					eController.cash -= pController.weapons[index].buyAmmoPrice;
					pController.weapons[index].ammo += pController.weapons[index].buyAmmoAmount;
				} else {
					Debug.Log ("Not Enough Cash");
				}
			}
		} else {
			// Try to buy gun
			if(eController.cash >= pController.weapons[index].buyPrice) {
				eController.cash -= pController.weapons[index].buyPrice;
				pController.weapons[index].own = true;
			} else {
				Debug.Log ("Not Enough Cash");
			}
		}
		if (pController.weapons [index].ammo > pController.weapons [index].maxAmmo) {
			pController.weapons[index].ammo = pController.weapons[index].maxAmmo;
		}
		openStore ();
	}

	public void onHealthButton(int i) {
		if (eController.cash >= (int)heal [i].y && (launchPad.maxHealth - launchPad.health) >= (int)heal [i].x) {
			// Heal
			eController.cash -= (int)heal [i].y;
			launchPad.health += (int)heal [i].x;
		} else {
			Debug.Log ("Can not heal");
		}
		openStore ();
	}

	public void onFortifyButton(int i) {
		if (eController.cash >= (int)fortify[i].y && (launchPad.maxHealth - (int)fortify[i].x) <= maxLaunchpadHealth) {
			// Heal
			eController.cash -= (int)fortify [i].y;
			launchPad.health += (int)fortify [i].x;
			launchPad.maxHealth += (int)fortify [i].x;
		} else {
			Debug.Log ("Can not fortify");
		}
		openStore ();
	}

	public void onMainMenu() {
		Application.LoadLevel (0);
	}

	public void onLose(int round) {
		youLosePanel.SetActive(true);
		tvroundLost.text = "You Lost On Round: " + round;
		pController.powerUpMultipler = 0;
		pController.powerUpMultiplerTill = Mathf.Infinity;
	}

}
