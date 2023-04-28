using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverListener : MonoBehaviour
{
	[SerializeField] private FishAI fish;
	
	public void startRecovery(){
		fish.recover();
	}
}
