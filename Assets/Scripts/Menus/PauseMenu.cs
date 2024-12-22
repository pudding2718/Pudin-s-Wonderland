using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Animator anim;

    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        anim = pauseMenuUI.GetComponent<Animator>();
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 0;
        pauseMenuUI.SetActive(false);
    }

    void Update(){
        if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
    }

    public void Pause(){
		IsGamePaused = true;
        pauseMenuUI.SetActive(true);
        anim.ResetTrigger("Trigger1");
        anim.SetTrigger("Trigger");
        //anim.Play("PauseMenuAnimation_Reverse");
    }

    public void Resume(){
        IsGamePaused = false;
        anim.ResetTrigger("Trigger");
        anim.SetTrigger("Trigger1");
        anim.Play("PauseMenuAnimation_Reverse");
        pauseMenuUI.SetActive(false);
    }

    public void Back(){
        SceneManager.LoadScene("MainMenu");
    }

	public void GamePaused()
	{
		Pause();
	}

}
