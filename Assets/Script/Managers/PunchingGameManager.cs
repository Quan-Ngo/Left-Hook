using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchingGameManager : MonoBehaviour
{
	[SerializeField] private GameObject boxer;
	[SerializeField] private GameObject fish;
	[SerializeField] private GameObject HPBar;
	[SerializeField] private GameObject fishingButton;  
	[SerializeField] private GameObject shopButton;

	public static PunchingGameManager instance;
	
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void startPunchingGame()
	{
		GameObject opponentFish;
		
		boxer.SetActive(true);
		HPBar.SetActive(true);
		opponentFish = Instantiate(fish);
		
		boxer.GetComponent<PlayerController>().fish = opponentFish.GetComponent<FishAI>();
		opponentFish.GetComponent<FishAI>().player = boxer.GetComponent<PlayerController>();
		opponentFish.GetComponent<FishAI>().healthBar = HPBar.GetComponent<Slider>();
	}
	
	public void fightComplete()
	{
		boxer.GetComponent<PlayerController>().endFight();
		HPBar.SetActive(false);
		FishingMinigameManager.instance.startFishing();

		fishingButton.SetActive(true);
		shopButton.SetActive(true);
	}
}
