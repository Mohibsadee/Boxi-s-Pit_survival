using UnityEngine;

public class PoisonTreeScript : MonoBehaviour
{

    private bool hasCollided = false;

    public ParticleSystem greenSmoke;
    private float deathTimeStart = 0f;
    private void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.CompareTag("Player")){
        Destroy(gameObject);
        Instantiate(greenSmoke, transform.position, Quaternion.identity);
        ScoreScript.scoreValue -= 10;
        PlayerMovement.isPoisonus = true;
        hasCollided = false;
    }
   }

   
   public void OnCollisionStay2D(Collision2D poisonTree){

       if(poisonTree.gameObject.layer== LayerMask.NameToLayer("Ground") ){
            
         //   Instantiate(stickParticleSystem, transform.position, Quaternion.identity);
        if (deathTimeStart == 0f)
        {
            deathTimeStart = Time.time;
        }
        
        if (Time.time - deathTimeStart > 10f)
        {   
            Instantiate(greenSmoke, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
            
        }
   }
   

}
