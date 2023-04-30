using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishAnimationListener : MonoBehaviour
{
	[SerializeField] private FishAI fish;
	[SerializeField] private PlayerController player;
	
	public void startRecovery()
	{
		fish.recover();
	}
	
	public void hitLeft()
	{
		player.getHit("left");
	}
	
	public void hitRight()
	{
		player.getHit("right");
	}
	
	public void hitMid()
	{
		player.getHit("mid");
	}
}
