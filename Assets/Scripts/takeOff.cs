using UnityEngine;
using System.Collections;

public class takeOff : MonoBehaviour {
	float lastSpeedIncrease = 0;
	private int increaseSpeedTimer = 1;
	private int increaseAmount = 0;
	public GameObject newRocket;

	// Use this for initialization
	void Start () {
		Invoke ("moveRocket", 25);
	}

	void moveRocket(){
		GameObject flame = GameObject.Find("flame");
		if (flame) {
			flame.particleSystem.Play();
		}

		rigidbody2D.velocity = new Vector2 (0,1);
		increaseAmount = 1;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > lastSpeedIncrease + increaseSpeedTimer) {
			rigidbody2D.velocity = new Vector2 (0,rigidbody2D.velocity.y + increaseAmount);
			lastSpeedIncrease = Time.time;
			increaseAmount = 0;
		}

		if (rigidbody2D.position.y > 15) {
			Instantiate(newRocket,new Vector3(-1.96f,3.79f,2),Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
