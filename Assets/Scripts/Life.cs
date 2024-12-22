using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{

	public Transform life;
	private int MaxLifeNum;

	void Start()
    {
		MaxLifeNum = 3;

	}

    void Update()
    {

	}

	public void hurt()
	{
		for (int i = 0; i < MaxLifeNum; i++)
		{
			if (!GetChild(life, 1).gameObject.activeInHierarchy)//To determine whether it is the last life
			{
				GetChild(life, 0).gameObject.SetActive(false);//Finally, health disappears
				Invoke("Restart", 0f);
			} else
			{
				if (GetChild(life, 2-i).gameObject.activeInHierarchy)
				{
					GetChild(life, 2 - i).gameObject.SetActive(false);//Health disappears at the end of the sequence
					return;
				}
			}
		}
	}
	public void recover()
	{
		for (int i = 0; i < MaxLifeNum; i++)
		{
			if (GetChild(life, MaxLifeNum-1).gameObject.activeInHierarchy)//Health is full
			{
				return;
			}
			else
			{
				if (!GetChild(life,i).gameObject.activeInHierarchy)
				{
					GetChild(life, i).gameObject.SetActive(true);//Life value + 1
					return;
				}
			}
		}
	}


	private Transform GetChild(Transform tr, int index)//Get child object
	{
		return tr.GetChild(index);
	}

	void Restart()//Reset the game
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Gets the name of the current scene to load when dead
	}
}
