using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerHealth : MonoBehaviour
{
    public static float health;
    public static float maxHealth = 100f;
    public ParticleSystem redSmoke;
    private float deathCounter = 0f;
    private float baseDamage= 1;
    private float announceTimeSart = 0f;

   
  


    void Start()
    {
        health = maxHealth;
   
    
       
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StoneToFall") )
        {
            Instantiate(redSmoke,collision.transform.position,Quaternion.identity);
            deathCounter = Time.time;
            TakeDamage(baseDamage);
        }
        if (collision.gameObject.CompareTag("Stone") )
        {
          
            TakeDamage(0);
        }
    }


    private void OnCollisionStay2D(Collision2D collision){


        if(collision.gameObject.CompareTag("StoneToFall")){

        float elapsedTime = Time.time - deathCounter;

        // Calculate the damage based on the elapsed time and any other factors.
        float calculatedDamage = CalculateDamage(elapsedTime);

        // Apply the calculated damage.
        TakeDamage(calculatedDamage);
        }
    }

    private float CalculateDamage(float elapsedTime) {
    float damageIncreaseRate = 0.000025f;  // Adjust this rate as needed.
    return baseDamage + damageIncreaseRate * elapsedTime;
    }

    void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {   
     
        Destroy(gameObject);
        float announceTimeSart = Time.time;
        CallEndGame();          
    }
    void CallEndGame(){
        if(Time.time - announceTimeSart > 2f){
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
           SceneManager.LoadScene("MainMenu");
            #endif

        }
    }
}
