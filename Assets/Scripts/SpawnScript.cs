using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

  public GameObject cubeToFall;
  public GameObject bigCubeToFall;
  public GameObject stoneToFall;
  public GameObject miscToFall;
  public int scoreCount = 0;
  private bool isDanger = false;


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
      scoreCount += 10;
      Debug.Log("Points: " + scoreCount);

      float randomXPosition = Random.Range(minXPosition, maxXPosition);
      Vector3 spawnPosition = new Vector3(randomXPosition, transform.position.y, transform.position.z);

      int randomInt = Random.Range(1, 101);
      if (randomInt < 33)
      {

    
        int randomCube = Random.Range(1, 101);

        if (randomCube < 45)
        {
          Instantiate(cubeToFall, spawnPosition, Quaternion.identity);

        }
        else if (randomCube > 46 && randomCube < 75)
        {
          Instantiate(bigCubeToFall, spawnPosition, Quaternion.identity);

        }
        else
        {
          isDanger = true;
          Debug.Log("isDanger set to true");
          Instantiate(stoneToFall, spawnPosition, Quaternion.identity);
        }

      }
      else
        Instantiate(miscToFall, spawnPosition , Quaternion.identity);
    }
    else
      isDanger = false;
  }
}
