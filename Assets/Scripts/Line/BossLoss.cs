using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLoss : MonoBehaviour
{
	public Transform BossHeath;
	public Transform Boss;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			BossHeath.gameObject.SetActive(false);
			Boss.gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}
