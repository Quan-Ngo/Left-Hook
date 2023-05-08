using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigameManager : MonoBehaviour
{
	private enum ButtonToMash {LEFT, RIGHT, DOWN}
	
	[SerializeField] private GameObject fishingBar;
	[SerializeField] private float buttonMashChangeRate;
	[SerializeField] private ButtonToMash buttonToMash;
	[SerializeField] private float defaultReelDistance;
	[SerializeField] private float fishResist;
	[SerializeField] private float reelStrength;
	[SerializeField] private float greenBonus;
	[SerializeField] private float redPenalty;
	[SerializeField] private float fishingBarRate;
	[SerializeField] private Text mashButtonDisplay;
	[SerializeField] private Text distanceDisplay;
	[SerializeField] private float timeLimit;
	
	private bool inFishingGame;
	private float reelDistance;
	
	public static FishingMinigameManager instance;
	
    // Start is called before the first frame update
    void Start()
    {
		if (instance != null)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		
		reelDistance = defaultReelDistance;
		StartCoroutine(waitForNextBite());
		inFishingGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFishingGame && ((Input.GetKeyDown(KeyCode.LeftArrow) && buttonToMash == ButtonToMash.LEFT) || (Input.GetKeyDown(KeyCode.RightArrow) && buttonToMash == ButtonToMash.RIGHT) || (Input.GetKeyDown(KeyCode.DownArrow) && buttonToMash == ButtonToMash.DOWN)))
        {
			reelDistance -= reelStrength;
			checkReelDistance();
        }
		distanceDisplay.text = "Fish distance: " + (int) reelDistance;
    }
	
	private void checkReelDistance()
	{
		if (reelDistance <= 0)
		{
			fishGameSuccess();
		}
	}
	
	private void fishGameSuccess()
	{
		StopAllCoroutines();
		
		mashButtonDisplay.enabled = false;
		distanceDisplay.enabled = false;
		inFishingGame = false;
		
		/*boxer.SetActive(true);
		fish.SetActive(true);
		HPBar.SetActive(true);*/
		
		//gameObject.SetActive(false);
		
		PunchingGameManager.instance.startPunchingGame();
	}
	
	public void timingResult(string color)
	{
		fishingBar.SetActive(false);
		switch (color)
		{
			case "Green":
				reelDistance -= greenBonus;
				checkReelDistance();
				break;
			case "Red":
				reelDistance += redPenalty;
				checkReelDistance();
				break;
		}
	}
	
	private void fishEscape()
	{
		StopAllCoroutines();
		mashButtonDisplay.enabled = false;
		distanceDisplay.enabled = false;
		StartCoroutine(waitForNextBite());
	}
	
	IEnumerator waitForNextBite()
	{
		yield return new WaitForSeconds(Random.Range(5, 8));
		
		reelDistance = defaultReelDistance;
		mashButtonDisplay.enabled = true;
		distanceDisplay.enabled = true;
        StartCoroutine(buttonMashChangeTimer());
		StartCoroutine(fishResistCalc());
		StartCoroutine(fishingBarPopUp());
		StartCoroutine(fishingTimeLimit());
	}
		
	
	IEnumerator buttonMashChangeTimer()
	{
		switch (Random.Range(0,3))
		{
			case 0:
				buttonToMash = ButtonToMash.LEFT;
				mashButtonDisplay.text = "Mash Left";
				break;
			case 1:
				buttonToMash = ButtonToMash.RIGHT;
				mashButtonDisplay.text = "Mash Right";
				break;
			case 2:
				buttonToMash = ButtonToMash.DOWN;
				mashButtonDisplay.text = "Mash Down";
				break;
		}
			
		while (true)
		{
			yield return new WaitForSeconds(buttonMashChangeRate);
			
			switch (Random.Range(0,3))
			{
				case 0:
					buttonToMash = ButtonToMash.LEFT;
					mashButtonDisplay.text = "Mash Left";
					break;
				case 1:
					buttonToMash = ButtonToMash.RIGHT;
					mashButtonDisplay.text = "Mash Right";
					break;
				case 2:
					buttonToMash = ButtonToMash.DOWN;
					mashButtonDisplay.text = "Mash Down";
					break;
			}
		}
	}
	
	IEnumerator fishResistCalc()
	{
		while (true)
		{
			yield return new WaitForSeconds((1/60));
			reelDistance += fishResist;
		}
	}
	
	IEnumerator fishingBarPopUp()
	{
		while (true)
		{
			yield return new WaitForSeconds(fishingBarRate);
			fishingBar.SetActive(true);
		}
	}
	
	IEnumerator fishingTimeLimit()
	{
		yield return new WaitForSeconds(timeLimit);
		fishEscape();
	}
	
	public void startFishing()
	{
		inFishingGame = true;
		StartCoroutine(waitForNextBite());
	}
}
