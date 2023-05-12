using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
	private enum Position {LEFT, RIGHT, MID}
	private bool animationLock;
	private bool lastPunchLeft = false; // Added this line to track the last punch direction
	private bool fishStunned;
	
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
		fishStunned = fish.IsStunned(); // Get the fish's stunLock status
		//player.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && (!animationLock || (fishStunned && !lastPunchLeft)))
        {
            playAnimation("left punch");
			fish.getHit(damage);
			lastPunchLeft = true; // Update the last punch direction
			
            if (fishStunned) // Add this block to end the animation lock early if fish is stunned
            {
                endAnimationLock();
            }
			fishStunned = fish.IsStunned();
        }
        else if (Input.GetButtonDown("Fire2") && (!animationLock || (fishStunned && lastPunchLeft)))
        {
			playAnimation("right punch");
			fish.getHit(damage);
			
			lastPunchLeft = false; // Update the last punch direction
            if (fishStunned) // Add this block to end the animation lock early if fish is stunned
            {
                endAnimationLock();
            }
			fishStunned = fish.IsStunned();
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
	
	public void getHit(string hitPos, int damage)
	{
		switch (hitPos)
		{
			case "left":
				if (dodgePosition == Position.LEFT)
				{
					takeDamage(damage);
				}
				break;
			case "right":
				if (dodgePosition == Position.LEFT)
				{
					takeDamage(damage);
				}
				break;
			case "mid":
				if (dodgePosition == Position.MID)
				{
					takeDamage(damage);
				}
				break;
		}
	}
	
	private void takeDamage(int damage)
	{
		currentHealth -= damage;
	}
	
	public void endFight()
	{
		endAnimationLock();
		resetDodgePosition();
		gameObject.SetActive(false);
	}
		
}
