using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy_Boss : MonoBehaviour
{
	public AIPath aiPath;
	public Transform aiGFX;

	private Rigidbody2D rb;
	private Collider2D Coll;
	private Animator Anim;
	public AudioSource deathAudio;
	public AudioSource hurtAudio;

	public int maxHealth = 3;
	int currentHealth;
	Vector2 lookDirection = new Vector2(1, 0);

	static float lastTime;
	public Transform BossHeath;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();//Get rigid body components at the start of the game
		Anim = GetComponent<Animator>();//Get animation components at the start of the game
		Coll = GetComponent<Collider2D>();//Get collider Y component at the start of the game

		currentHealth = maxHealth;
		lastTime = Time.time;
	}

	void Update()
	{
		if (currentHealth < 1){
			Death();
			ResetHealth();
			BossHeath.parent.gameObject.SetActive(false);
		}
		if (aiPath.desiredVelocity.x >= 0.01f)
		{
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		else if (aiPath.desiredVelocity.x <= 0.01f)
		{
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (Time.time - lastTime > 2.0f)
		{
			SwitchAnim();
			lastTime = Time.time;
		}
	}

	void SwitchAnim()
	{
		if (Anim.GetBool("sc"))
		{
			Anim.SetBool("sc", false);
			aiGFX.GetComponent<AIDestinationSetter>().enabled = true;
		}
		else {
			Anim.SetBool("sc", true);
			aiGFX.GetComponent<AIDestinationSetter>().enabled = false;
		}
	}
	public void ChangeHealth(int amount)
	{
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		hurtAudio.Play();
		UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
	}
	public void ResetHealth()
	{
		currentHealth = maxHealth;
		UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
	}

	public void Death()
	{
		Anim.SetTrigger("death");
		deathAudio.Play();
		StartCoroutine(Wait(0.5f));
	}

	IEnumerator Wait(float t)
	{
		yield return new WaitForSeconds(t);
		Destroy(gameObject);
	}
}
