using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
   

    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
