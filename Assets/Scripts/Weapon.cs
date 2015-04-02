using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon {

	public string name;
	public float damage;
	public enum FireType {Semi, Auto};
	public FireType fireType;
	public float fireRate;
	public bool collateral;


}
