using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D rb;//Get the Rigidbody component
	private Animator anim;//Get the Animator component
	public Collider2D coll;//Get the collider component
	public Collider2D Discoll;//Gets a closed collider component
	public AudioSource jumpAudio;
	public AudioSource hurtAudio;
	public AudioSource coinAudio;
	public AudioSource deathAudio;
	public Transform CellingCheck;//Test the ceiling for anything
    private int Score;//Harvest points for defeating enemies
	public bool isHurt;//Determine if there is an injury

	public float speed;//Define how fast the player moves
	public float jumpforce;//Define the force by which the player jumps
	public LayerMask ground;//Get the defined layer ground
    public Text ScoreNum;//Display enemy defeat scores in UI mode

	public Joystick joystick;
	bool jump = false;
	bool crouch = false;

	void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Get rigid body components at the start of the game
		anim = GetComponent<Animator>();//Get animation components at the start of the game
		Score = PlayerPrefs.GetInt("Score", 0);
		speed = PlayerPrefs.GetInt("speed", 400);
		jumpforce = PlayerPrefs.GetInt("jumpforce", 900);
		ScoreNum.text = Score.ToString();
	}

    void FixedUpdate()
    {
        if (!isHurt)//If you don't get hurt
		{
            Movement();//Calls to Movement

		}
        SwitchAnim();//Call the jump drop toggle animation
		Jump();
	}

    void Movement()//Move the script
	{
  //      float horizontalmove = Input.GetAxis("Horizontal");//It's between minus one and minus one
		//float facedirection = Input.GetAxisRaw("Horizontal");//Minus one, zero, one. Three values

		float horizontalmove = joystick.Horizontal;//It's between minus one and minus one
		float facedirection = joystick.Horizontal;//Minus one, zero, one. Three values

		if (horizontalmove != 0)//Gets the left and right directions of keyboard input
		{
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);//Change the value of x at the 2D level to achieve horizontal movement
			anim.SetFloat("running", Mathf.Abs(horizontalmove));//When the speed of the player is greater than 0.1, it enters the running state, otherwise it enters the standing state
		}

        if (facedirection > 0)//Gets the left and right directions of keyboard input
		{
            transform.localScale = new Vector3(1, 1, 1);//When scale is negative one, the character turns to the right, and when scale is one, the character turns to the left
		}

		if (facedirection == 0)//Gets the left and right directions of keyboard input
		{
			anim.SetFloat("running", 0);
			anim.SetBool("idle", true);
		}

		if (facedirection < 0)//Gets the left and right directions of keyboard input
		{
			transform.localScale = new Vector3(-1, 1, 1);//When scale is negative one, the character turns to the right, and when scale is one, the character turns to the left
		}

		if ((Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) || (jump && coll.IsTouchingLayers(ground)))//Gets input for keyboard Spaces
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);//Change the value of y on the 2D level to achieve vertical movement
			jumpAudio.Play();
			anim.SetBool("jumping", true);//If it is a jump, the jump animation will be converted
			jump = false;
		}

		Crouch();

    }
	//Character switching animation
	void SwitchAnim()//Switch between jumping animation and falling animation
	{
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))//If the speed of ascent is less than 0.1, perform the fall animation
		{
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))//If you are in the jump animation state
		{
            if (rb.velocity.y < 0)//If you're moving at a speed less than zero, you're stationary
			{
                anim.SetBool("jumping", false);//Turn off jump animation
				anim.SetBool("falling", true);//Turn on the falling animation
			}
        }
        else if(isHurt)//If the injured
		{
            anim.SetBool("hurt", true);//Apply injury animation
			anim.SetFloat("running", 0);//Change the run animation to 0, that is, not executed
			if (Mathf.Abs(rb.velocity.x) < 0.1f)//Return to normal when the speed of movement after injury is less than 0.1
			{

                anim.SetBool("hurt", false );
                anim.SetBool("idle", true);
                isHurt = false;//Set it to false to perform the move function
			}
        }
        else if (coll.IsTouchingLayers(ground))//Whether the collider touches the ground layer
		{
			anim.SetBool("falling", false);//Turn off the falling state
			anim.SetBool("idle", true);//To stand
		}
    }
	//Collide with each other
	private void OnTriggerEnter2D(Collider2D collision)//Collider function
	{
		//Role to collect
		if (collision.tag == "Cherry")//If the tag is Cherry, destroy the object it hit
		{
			coinAudio.Play();
			Destroy(collision.gameObject);//Destroy whatever you hit
			GameObject.Find("ScriptsManagers").GetComponent<Life>().recover();//Recover is called with +1 health
		}

		if (collision.tag == "DeadLine")//If you meet death
		{
			deathAudio.Play();
			Invoke("Restart", 2f);//The reset function is delayed for two seconds
		}
		if (collision.tag == "NextLine")
		{
			PlayerPrefs.SetInt("Score", Score);
		}
	}
	//Destroy the enemy
	private void OnCollisionEnter2D(Collision2D collision)//Collider function
	{
        if (collision.gameObject.tag == "Enemies")//If you hit an enemy
		{
			Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>();//Class defines an object that gets enemy child components
			Enemy_Eagle eagle = collision.gameObject.GetComponent<Enemy_Eagle>();//Class defines an object that gets enemy child components
			if (anim.GetBool("falling"))//The following statement is executed if an enemy is hit
			{
				Destroy(collision.gameObject.GetComponent<MeshCollider>());
				Score += 10;//Add 10 to each enemy you kill
				ScoreNum.text = Score.ToString();//Assign the value to ScoreNum on the UI
				if (frog != null) {
					frog.JumpOn();
				}
				if (eagle != null) {
					eagle.JumpOn();
				}
				//Destroy(collision.gameObject);//Destroy the enemy
				rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);//Change the value of y on the 2D level to achieve vertical movement
				jumpAudio.Play();
				anim.SetBool("jumping", true);//If it is a jump, the jump animation will be converted
			}
            else if (transform.position.x < collision.gameObject.transform.position.x)//If the character is to the left of the enemy
			{
                rb.velocity = new Vector2(-10, rb.velocity.y);//We're going to go 10 to the left
				hurtAudio.Play();
				isHurt = true;
				GameObject.Find("ScriptsManagers").GetComponent<Life>().hurt();//Call the hurt function, health -1
			}
            else if (transform.position.x > collision.gameObject.transform.position.x)//If the character is to the right of the enemy
			{
                rb.velocity = new Vector2(10, rb.velocity.y);//We're going to go 10 to the right
				hurtAudio.Play();
				isHurt = true;
				GameObject.Find("ScriptsManagers").GetComponent<Life>().hurt();//Call the hurt function, health -1
			}
        }
		if (collision.gameObject.tag == "Boss")//If you hit an enemy
		{
			Animator BossAnim = collision.gameObject.GetComponent<Animator>();
			Enemy_Boss controller = collision.gameObject.GetComponent<Enemy_Boss>();
			if (anim.GetBool("falling"))//The following statement is executed if an enemy is hit
			{
				if (BossAnim.GetBool("sc"))
				{
					hurtAudio.Play();
					rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);//Change the value of y on the 2D level to achieve vertical movement
					anim.SetBool("jumping", true);//If it is a jump, the jump animation will be converted
					isHurt = true;
					GameObject.Find("ScriptsManagers").GetComponent<Life>().hurt();//Call the hurt function, health -1
				}
				else {
					controller.ChangeHealth(-1);
					Score += 10;//Add 10 to each enemy you kill
					ScoreNum.text = Score.ToString();
					rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);//Change the value of y on the 2D level to achieve vertical movement
					jumpAudio.Play();
					anim.SetBool("jumping", true);//If it is a jump, the jump animation will be converted
				}
				
			}
			else if (transform.position.x < collision.gameObject.transform.position.x)//If the character is to the left of the enemy
			{
				rb.velocity = new Vector2(-10, rb.velocity.y);//We're going to go 10 to the left
				hurtAudio.Play();
				isHurt = true;
				GameObject.Find("ScriptsManagers").GetComponent<Life>().hurt();//Call the hurt function, health -1
			}
			else if (transform.position.x > collision.gameObject.transform.position.x)//If the character is to the right of the enemy
			{
				rb.velocity = new Vector2(10, rb.velocity.y);//We're going to go 10 to the right
				hurtAudio.Play();
				isHurt = true;
				GameObject.Find("ScriptsManagers").GetComponent<Life>().hurt();//Call the hurt function, health -1
			}
		}
	}
    public void Jump()
    {
        if ((Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) || (jump && coll.IsTouchingLayers(ground)))//Gets input for keyboard Spaces
		{
			jumpAudio.Play();
			rb.velocity = new Vector2(0, jumpforce * Time.deltaTime);//Change the value of y on the 2D level to achieve vertical movement
			anim.SetBool("jumping", true);//If it is a jump, the jump animation will be converted
			jump = false;
		}
    }

	public void JumpButtonDown()
	{
		jump = true;
	}
	public void CrouchButtonDown()
	{
		crouch = true;
	}
	public void CrouchButtonUp()
	{
		crouch = false;
	}


	public void Crouch()//Get down the animation
	{
			if (Input.GetButton("Crouch")|| crouch)//If you press the S key, perform the lie down animation
			{
				//Debug.Log("Squat down");
				anim.SetBool("crouching", true);
				Discoll.enabled = false;
			}
			else//If you release the S key and do not execute the lie down animation, return to the original animation
			{
				anim.SetBool("crouching", false);
				Discoll.enabled = true;
			}

	}

    void Restart()//Reset the game
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Gets the name of the current scene to load when dead

	}
}
