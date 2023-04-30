using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private enum Position {LEFT, RIGHT, MID}
	
	private Position dodgePosition;
	private bool animationLock;
	
    public Animator anim;
	public FishAI fish;
	public int damage;
	
    // Start is called before the first frame update
    void Start()
    {
        
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
}
