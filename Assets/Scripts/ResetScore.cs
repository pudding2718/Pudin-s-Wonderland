using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScore : MonoBehaviour
{
	public Text ScoreNum;//Display enemy defeat scores in UI mode

	void Awake()
    {
		PlayerPrefs.SetInt("Score", 0);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
