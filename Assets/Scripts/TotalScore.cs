using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
	public Text ScoreNum;//Display enemy defeat scores in UI mode

	void Start()
    {
		int Score = PlayerPrefs.GetInt("Score", 0);
		ScoreNum.text = Score.ToString();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
