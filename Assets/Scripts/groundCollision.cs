using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public ParticleSystem stickParticleSystem;
   private void OnCollisionEnter2D(Collision2D misc){
        if(misc.gameObject.layer== LayerMask.NameToLayer("Ground") ){
            
            Instantiate(stickParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
   }
}
