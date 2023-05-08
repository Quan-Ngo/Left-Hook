using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
	private enum Position {LEFT, RIGHT, MID}
	private bool animationLock;
	
	[SerializeField] private Position dodgePosition;
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;
	
    public Animator anim;
	public FishAI fish;
	public int damage;
	public GameObject losePanel;
	//public GameObject player; 
	
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
		dodgePosition = Position.MID;
		losePanel.SetActive(false);
		//player.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !animationLock)
        {
            playAnimation("left punch");
			fish.getHit(damage);
        }
        else if (Input.GetButtonDown("Fire2") && !animationLock)
        {
			playAnimation("right punch");
			fish.getHit(damage);
        }
		else if (Input.GetKeyDown(KeyCode.LeftArrow) && !animationLock)
		{
			playAnimation("left dodge");
			dodgePosition = Position.LEFT;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow) && !animationLock)
		{
			playAnimation("right dodge");
			dodgePosition = Position.RIGHT;
		}


		if (currentHealth <= 0)
		{
			Debug.Log("You died");
			losePanel.SetActive(true);
			gameObject.SetActive(false); 
			
		}
    }
	
	private void playAnimation(string animation)
	{
		animationLock = true;
		
		switch (animation)
		{
			case "left punch":
				anim.SetTrigger("Left Punch");
				break;
			case "right punch":
				anim.SetTrigger("Right Punch");
				break;
			case "left dodge":
				anim.SetTrigger("Left Dodge");
				break;
			case "right dodge":
				anim.SetTrigger("Right Dodge");
				break;
		}
	}
	
	public void endAnimationLock()
	{
		animationLock = false;
	}
	
	public void resetDodgePosition()
	{
		dodgePosition = Position.MID;
	}
	
	public void getHit(string hitPos)
	{
		switch (hitPos)
		{
			case "left":
				if (dodgePosition == Position.LEFT)
				{
					takeDamage();
				}
				break;
			case "right":
				if (dodgePosition == Position.LEFT)
				{
					takeDamage();
				}
				break;
			case "mid":
				if (dodgePosition == Position.MID)
				{
					takeDamage();
				}
				break;
		}
	}
	
	private void takeDamage()
	{
		currentHealth -= 50;
	}
	
	public void endFight()
	{
		endAnimationLock();
		resetDodgePosition();
		gameObject.SetActive(false);
	}
		
}
