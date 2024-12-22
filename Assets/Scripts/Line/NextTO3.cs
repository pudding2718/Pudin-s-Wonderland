using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextTO3 : MonoBehaviour
{
	public AudioSource winAudio;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	private void OnTriggerEnter2D(Collider2D collision)//Collider function
	{
		if (collision.tag == "Player")
		{
			winAudio.Play();
			SceneManager.LoadScene("Scene3");
		}
	}
}
