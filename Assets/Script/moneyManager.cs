using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
	public static moneyManager instance;
	
	[SerializeField] private int playerMoney;
	
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad();
		}
		else
		{
			Destroy(this);
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void addMoney(int money)
	{
		playerMoney += money;
	}
}
