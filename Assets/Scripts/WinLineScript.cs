
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinLineScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.CompareTag("Player")){

            Debug.Log("You Win");
            EndGame();
        }

    }
    public void EndGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        SceneManager.LoadScene("MainMenu");
        #endif
    }
}
