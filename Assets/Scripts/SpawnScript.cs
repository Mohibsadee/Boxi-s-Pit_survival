using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

  public GameObject cubeToFall;
  public GameObject bigCubeToFall;
  public GameObject stoneToFall;//stone that spawns
  public GameObject stoneToTurn;//stone that I want to turn after 3 sec
  public GameObject miscToFall;
  public GameObject goldOre;
  public GameObject poisonTree;

  public ParticleSystem stoneParticleSystem;
 
  private bool isDanger = false;
  

  private float stoneCoolDownStart = 0f;
  private GameObject newStone;

  private List<GameObject> stoneToChange = new List<GameObject>();
 /*  private float delayBetweenChanges = 3f;
  private int currentStoneIndex = 0;
 */

  private float minXPosition = -7f; // Minimum X position
  private float maxXPosition = 7f;  // Maximum X position

  public bool IsDanger()
  {
    return isDanger;
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
    
  

      float randomXPosition = Random.Range(minXPosition, maxXPosition);
      Vector3 spawnPosition = new Vector3(randomXPosition, transform.position.y, transform.position.z);

      int randomInt = Random.Range(1, 101);
      if (randomInt < 45)//33
      {


        int randomCube = Random.Range(1, 101);

        if (randomCube < 1)//45
        {
          Instantiate(cubeToFall, spawnPosition, Quaternion.identity);

        }
        else if (randomCube > 6 && randomCube < 7)//46 55
        { 
          Instantiate(bigCubeToFall, spawnPosition, Quaternion.identity);

        }


        else if (randomCube > 8 && randomCube < 9)//56 75
        { 
        
          Instantiate(poisonTree, spawnPosition, Quaternion.identity);
       

        }

        else
        {
          
          isDanger = true;
          newStone = Instantiate(stoneToFall, spawnPosition, Quaternion.identity);
          stoneCoolDownStart = Time.time;


      /*     Debug.Log("isDanger set to true");
          var newStone = Instantiate(stoneToFall, spawnPosition, Quaternion.identity);
          stoneToChange.Add(newStone);  */
          
        }

      }
      else
        Instantiate(miscToFall, spawnPosition, Quaternion.identity);
    }
    else{
      isDanger = false;
    }

  /*   for(int i = stoneToChange.Count-1 ;i>=0;i--){
      
      if (stoneToChange.Count>0 && Time.time - stoneCoolDownStart > delayBetweenChanges)
      {
        if(currentStoneIndex < stoneToChange.Count){
          var oldStone = stoneToChange[currentStoneIndex];
          stoneToChange.RemoveAt(currentStoneIndex);
          Destroy(oldStone); // Destroy the old stone
          Instantiate(stoneToTurn, oldStone.transform.position, Quaternion.identity);
          currentStoneIndex++;
        } 
      } */

      if (newStone != null && Time.time - stoneCoolDownStart > 3f)
        {
            Destroy(newStone); // Destroy the old stone
            var stonePos = Instantiate(stoneToTurn, newStone.transform.position, Quaternion.identity); // Instantiate the new stone
            Instantiate(stoneParticleSystem, stonePos.transform.position, Quaternion.identity);

            int goldChance = Random.Range(1, 101);
            if(goldChance > 50)
             
              Instantiate(goldOre, new Vector3(newStone.transform.position.x, newStone.transform.position.y + 0.2f, newStone.transform.position.z), Quaternion.identity);
            
        }
      
  }
}
