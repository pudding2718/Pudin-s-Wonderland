using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
  
    public LayerMask Ground;//Get the defined layer ground
	public Transform leftpoint,rightpoint;//Gets left and right boundary points
	public float Speed,JumpForce;//Define speed and spring
	public AudioSource deathAudio;

    private float leftx,rightx;//Definition for receiving left and right points
	private Rigidbody2D rb;//Get the Rigidbody component
	private Animator Anim;//Get the Animator component
	private Collider2D Coll;//Gets the collider component
	private bool Faceleft = true;//The conditions that made the frog turn left in the first place were correct

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Get rigid body components at the start of the game
		Anim = GetComponent<Animator>();//Get animation components at the start of the game
		Coll = GetComponent<Collider2D>();//Get collider components at the start of the game

		leftx = leftpoint.position.x;//Get the coordinates of the left critical point
		rightx = rightpoint.position.x;//Get the coordinates of the right critical point
		transform.DetachChildren();//Cancelled the parent-child relationship of frog left and right points
		Destroy(leftpoint.gameObject);//To prevent too many critical points per frame, delete one at a time
		Destroy(rightpoint.gameObject);//To prevent too many critical points per frame, delete one at a time
	}

    void Update()
    {
        SwitchAnim();//Call the animation toggle function
	}
    
    void Movement()//Mobile function
	{
        if(Faceleft)//When facing to the left
		{
            if (Coll.IsTouchingLayers(Ground))//When the frog touches the ground
			{
                Anim.SetBool("jumping", true);//Perform jump animation
				rb.velocity = new Vector2(-Speed,JumpForce);//Frog moves to the left
			}
            if (transform.position.x < leftx)//When the position of the frog exceeds the position of the left boundary point
			{
               transform.localScale = new Vector3(-1,1,1);//Complete the frog's right turn
				Faceleft = false;
            }
         }
		else//Otherwise facing to the right
		{
            if (Coll.IsTouchingLayers(Ground))//When the frog touches the ground
			{
                Anim.SetBool("jumping", true);//Perform jump animation
				rb.velocity = new Vector2(Speed,JumpForce);//The frog moved right
			}
            if (transform.position.x > rightx)//When the frog's position exceeds the position of the right boundary point
			{
               transform.localScale = new Vector3(1,1,1);//Complete the frog's left turn
				Faceleft = true;
            }
         }
    }

    void SwitchAnim() //Animation switch script
	{
        if (Anim.GetBool("jumping"))//If you're jumping right now
		{ 
            if (rb.velocity.y < 0.1)//When the speed of jump up is less than 0.1
			{
                Anim.SetBool("jumping", false);//Turn off the jump animation
				Anim.SetBool("falling", true);//Turn on the falling animation
			}
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))//When the frog touches the ground and the falling animation turns off
		{
            Anim.SetBool("falling", false);//To complete the animation loop event, you need to complete the transition from the fall animation to the initial state
		}
    }

    public void Death()
    {
		Destroy(gameObject);//Destroy the enemy
	}

    public void JumpOn()
    {
		deathAudio.Play();
		Anim.SetTrigger("death");
		StartCoroutine(Wait(0.5f));
	}
	IEnumerator Wait(float t)
	{
		yield return new WaitForSeconds(t);
		Destroy(gameObject);
	}
}
