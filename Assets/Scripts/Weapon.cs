using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {

	public string name;
	public float damage;
	public enum FireType {Semi, Auto};
	public FireType fireType;
	public float fireRate;
	public enum DamageType {Normal, Collateral, Splash};
	public DamageType damageType;
	public float splashRadius;
	public int ammo;
	public int maxAmmo;
	public int clipAmmo;
	public int clipSize;
	public Sprite muzzleFlash;
	public float reloadTime;
	public bool own;
	public int buyPrice;
	public int buyAmmoPrice;
	public int buyAmmoAmount;
	public AudioClip audio;


}
