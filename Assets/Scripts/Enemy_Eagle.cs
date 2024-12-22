using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_Eagle : MonoBehaviour
{
	public AIPath aiPath;

    private Rigidbody2D rb;
    private Collider2D Coll;
    private Animator Anim;

    public Transform top, bottom;
    public float Speed;
    private float TopY,BottomY;
    public AudioSource deathAudio;

    private bool isUp;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Get rigid body components at the start of the game
		Anim = GetComponent<Animator>();//Get animation components at the start of the game
		Coll = GetComponent<Collider2D>();//Get collider Y component at the start of the game
		transform.DetachChildren();//Cancelled the parent-child relationship of eagle's upper and lower points
		TopY = top.position.y;//Get the coordinates of the upper critical point
		BottomY = bottom.position.y;//Get the coordinates of the critical points
		Destroy(top.gameObject);//To prevent too many critical points per frame, delete one at a time
		Destroy(bottom.gameObject);//To prevent too many critical points per frame, delete one at a time
	}

    void Update()
    {
		if (aiPath.desiredVelocity.x >= 0.01f) {
			transform.localScale = new Vector3(-1f, 1f, 1f);
		} else if (aiPath.desiredVelocity.x <= 0.01f) {
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
        //Movement();
    }
    void Movement()
    {  
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);//The eagle moves up
			if (transform.position.y > TopY)
            {
                isUp = false;
                rb.velocity = new Vector2(rb.velocity.x, -Speed);
            }
        }
        else 
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);//The eagle moves up
			if (transform.position.y < BottomY)
            {
                isUp = true;
                rb.velocity = new Vector2(rb.velocity.x, Speed);
            }
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
