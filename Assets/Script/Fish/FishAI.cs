using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class FishAI : MonoBehaviour
{
	protected enum BlockStates {ALL, NONE};
	
	[SerializeField] protected int maxHealth;
	[SerializeField] protected float timeBetweenAttacks;
	[SerializeField] protected Animator animator;
	[SerializeField] protected BlockStates blockState;
	[SerializeField] protected bool stunLock;
	[SerializeField] protected float stunLockDuration;
	[SerializeField] protected int stunThreshhold;
	[SerializeField] protected int currentStunVal;
	[SerializeField] protected int moneyValue;
	[SerializeField] protected Slider healthBar;
	
	protected int currentHealth;

	public GameObject winPanel; 
	
    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		currentStunVal = 0;
        StartCoroutine(neutralState());
		winPanel.SetActive(false);
    }

    private void Update()
    {
        if (currentHealth <= 0)
		{
			Debug.Log("You won!");
            stunLock = true;
            winPanel.SetActive(true);
		
		}
    }

    IEnumerator neutralState()
	{
		blockState = BlockStates.ALL;
		if (maxHealth > 0){
			yield return new WaitForSeconds(timeBetweenAttacks + Random.Range(-0.5f, 0.5f));
			attack();
		}
	}
	
	public void recover()
	{
		if (!stunLock)
		{
			animator.SetTrigger("Neutral");
			StartCoroutine(neutralState());
		}
	}
	
	public void getHit(int damage)
	{
		if (stunLock)
		{
			takeDamage(damage);
		}
		else
		{
			StopAllCoroutines();
			switch (blockState)
			{
				case BlockStates.ALL:
					animator.SetTrigger("Block");
					break;
				case BlockStates.NONE:
					takeDamage(damage);
					checkStun();
					break;
			}
		}
	}
	
	IEnumerator stunLockCountDown()
	{
		stunLock = true;
		currentStunVal = 0;
		yield return new WaitForSeconds(stunLockDuration);
		stunLock = false;
		recover();
	}
	
	private void takeDamage(int damage)
	{
		animator.SetTrigger("GetHit");
		currentHealth -= damage;
		healthBar.value = (float) currentHealth / (float) maxHealth;
	}
	
	private void checkStun()
	{
		currentStunVal++;
		if (currentStunVal == stunThreshhold)
		{
			StartCoroutine(stunLockCountDown());
		}
		else
		{
			blockState = BlockStates.ALL;
		}
	}
	
	protected abstract void attack();
}
