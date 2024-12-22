using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleGo : MonoBehaviour
{
	public Transform Eagle;
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
			Eagle.gameObject.SetActive(true);
			Destroy(gameObject);
		}
	}

}
