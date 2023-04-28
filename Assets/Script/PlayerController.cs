using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Left Punch");
			fish.getHit(damage);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Right Punch");
			fish.getHit(damage);
        }
    }
}
