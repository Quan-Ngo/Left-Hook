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
	[SerializeField] private float reelDistance;
	[SerializeField] private float fishResist;
	[SerializeField] private float reelStrength;
	[SerializeField] private float greenBonus;
	[SerializeField] private float redPenalty;
	[SerializeField] private float fishingBarRate;
	[SerializeField] private Text mashButtonDisplay;
	[SerializeField] private Text distanceDisplay;
	
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(buttonMashChangeTimer());
		StartCoroutine(fishResistCalc());
		StartCoroutine(fishingBarPopUp());
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) && buttonToMash == ButtonToMash.LEFT) || (Input.GetKeyDown(KeyCode.RightArrow) && buttonToMash == ButtonToMash.RIGHT) || (Input.GetKeyDown(KeyCode.DownArrow) && buttonToMash == ButtonToMash.DOWN))
        {
			reelDistance -= reelStrength;
        }
		distanceDisplay.text = "Fish distance: " + (int) reelDistance;
    }
	
	public void timingResult(string color)
	{
		fishingBar.SetActive(false);
		switch (color)
		{
			case "Green":
				reelDistance -= greenBonus;
				break;
			case "Red":
				reelDistance += redPenalty;
				break;
		}
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
}
