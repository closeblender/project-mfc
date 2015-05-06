using UnityEngine;
using System.Collections;

public class LaunchPadController : HealthController
{

	public MenuController menu;
	public EnemySpawnerController e;
	
	public override void die ()
	{
		Debug.Log ("You Lose!");
		menu.onLose (e.round + 1);

	}
}
