using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void GoToCredits(){
        SceneManager.LoadScene("CreditsMenu");
    }

    public void GoToSettings(){
        SceneManager.LoadScene("SettingsMenu");
    }

    public void QuitGame()
    {
		PlayerPrefs.DeleteKey("Score");
		PlayerPrefs.DeleteKey("speed");
		PlayerPrefs.DeleteKey("jumpforce");
		Application.Quit();
    }
}
