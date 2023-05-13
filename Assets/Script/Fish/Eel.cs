using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : FishAI
{
	private bool electrified;
	
    // Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
        electrified = false;
    }

	public override void recover()
	{
		if (electrified)
		{
			animator.SetTrigger("ElecNeutral");
			StartCoroutine(neutralState());
		}
		else
		{
			base.recover();
		}
	}
	
    protected override void attack()
	{
		int chosenAttack;
		blockState = BlockStates.NONE;
		
		if (!electrified)
		{
			chosenAttack = Random.Range(0, 7);
			if (chosenAttack <= 2)
			{
				animator.SetTrigger("PunchR");
			}
			else if (chosenAttack <= 5 && chosenAttack > 2)
			{
				animator.SetTrigger("PunchL");
			}
			else
			{
				electrified = true;
				recover();
			}
		}
		else
		{
			chosenAttack = Random.Range(0, 2);
			switch (chosenAttack)
			{
				case 0:
					animator.SetTrigger("UpperCutR");
					break;
				case 1:
					animator.SetTrigger("UpperCutL");
					break;
			}
		}
	}
}
