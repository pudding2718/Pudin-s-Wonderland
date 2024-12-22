using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeButton : MonoBehaviour
{
	public Button m_Button;
	public Transform Slider;
	// Start is called before the first frame update
	void Start()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
		m_Button.onClick.AddListener(ButtonOnClickEvent);
	}
	public void ButtonOnClickEvent()
	{
		if (Slider.gameObject.activeSelf)
		{
			Slider.gameObject.SetActive(false);
		}else 
		Slider.gameObject.SetActive(true);
	}
}
