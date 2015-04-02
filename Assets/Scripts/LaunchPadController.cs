using UnityEngine;
using System.Collections;

public class LaunchPadController : HealthController
{
	
	public override void die ()
	{
		Debug.Log ("You Lose!");
	}
}
