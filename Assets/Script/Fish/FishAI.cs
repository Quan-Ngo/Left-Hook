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
	[SerializeField] protected int damage;
	
	protected int currentHealth;

	public Slider healthBar;
	public PlayerController player;
	
    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		currentStunVal = 0;
		healthBar.value = (float) currentHealth / (float) maxHealth;
		stunLock = false;
        StartCoroutine(neutralState());
    }

    private void Update()
    {
        
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
		
		if (currentHealth <= 0)
		{
			Death();
		}
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
	
	public void Death()
	{
		Debug.Log("You won!");
		moneyManager.instance.addMoney(moneyValue);
		PunchingGameManager.instance.fightComplete();
		Destroy(gameObject);
	}
	
	public void hitPlayer(string hitPos)
	{
		player.getHit(hitPos, damage);
	}
	
	public bool IsStunned()
	{
		return stunLock;
	}
	
	protected abstract void attack();
}
