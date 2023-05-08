using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAnimationListener : MonoBehaviour
{
	[SerializeField] private FishAI fish;
	//[SerializeField] private PlayerController player;
	
	public void startRecovery()
	{
		fish.recover();
	}
	
	public void hitLeft()
	{
		fish.hitPlayer("left");
		//player.getHit("left");
	}
	
	public void hitRight()
	{
		fish.hitPlayer("right");
		//player.getHit("right");
	}
	
	public void hitMid()
	{
		fish.hitPlayer("mid");
		//player.getHit("mid");
	}
}
