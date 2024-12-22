using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	static int speed = 400;//Define how fast the player moves
	static int jumpforce = 900;//Define the force by which the player jumps
	private int Score;//Harvest points for defeating enemies

	public Text speedNum;
	public Text jumpNum;

	void Start()
	{
		PlayerPrefs.SetInt("speed", speed);
		PlayerPrefs.SetInt("jumpforce", jumpforce);
	}
	public void speedDown() {
		speed = speed - 50;
		PlayerPrefs.SetInt("speed", speed);
		//speedNum.text = speed.ToString();
	}
	public void speedUp()
	{
		speed = speed + 50;
		PlayerPrefs.SetInt("speed", speed);
		//speedNum.text = speed.ToString();
	}
	public void jumpDown()
	{
		jumpforce = jumpforce - 50;
		PlayerPrefs.SetInt("jumpforce", jumpforce);
		//jumpNum.text = jumpforce.ToString();
	}
	public void jumpUp()
	{
		jumpforce = jumpforce + 50;
		PlayerPrefs.SetInt("jumpforce", jumpforce);
		//jumpNum.text = jumpforce.ToString();
	}
	// Start is called before the first frame update


	// Update is called once per frame
	void Update()
    {
		speed = PlayerPrefs.GetInt("speed", speed);
		jumpforce = PlayerPrefs.GetInt("jumpforce", jumpforce);

		limit();

		speedNum.text = speed.ToString();
		jumpNum.text = jumpforce.ToString();
	}


	public void limit() {
		if (speed > 500)
		{
			speed = 500;
		}
		if (speed < 400)
		{
			speed = 400;
		}
		if (jumpforce > 1000)
		{
			jumpforce = 1000;
		}
		if (jumpforce < 800)
		{
			jumpforce = 800;
		}
	}
}
