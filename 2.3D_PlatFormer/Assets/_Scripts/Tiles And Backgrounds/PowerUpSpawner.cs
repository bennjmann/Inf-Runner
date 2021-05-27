using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour {
  public List<GameObject> m_PowerUps;

  public Renderer m_SpawnBounds;
  
  private void OnEnable() {
      int random = Random.Range(1, 4);
      if (random == 2) {                                                                                                                                // Chance to spawn
          GameObject powerUp = Instantiate(m_PowerUps[0], transform.position, Quaternion.identity) as GameObject;
          // Area to Spawn Within
          float randomZ = Random.Range(-m_SpawnBounds.bounds.extents.z, m_SpawnBounds.bounds.extents.z) + transform.parent.position.z;                 
          float randomX = Random.Range(-m_SpawnBounds.bounds.extents.x - 2, m_SpawnBounds.bounds.extents.x - 2 ) + transform.parent.position.x;
          powerUp.transform.position = new Vector3(randomX, 20, randomZ);       // Spawns at Random location in the air within Spawn area above.
          // Cast a Ray Down from the random position, Sets objects position to hit location and Normal + extent of power up.
          RaycastHit hit;
          var transformPosition = powerUp.transform.position;
          var rayCast = Physics.Raycast(transformPosition,Vector3.down, out hit);
          Debug.DrawRay(transformPosition,Vector3.down * 20, Color.green, 1000000);
          if (rayCast) {
              transformPosition.y = hit.point.y + powerUp.GetComponentInChildren<Renderer>().bounds.extents.y;
              powerUp.transform.position = transformPosition;
              powerUp.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
              Debug.DrawRay(transformPosition, hit.normal, Color.red, 10000);
          } else { Destroy(powerUp);}                                                                       // Removes power up if ray doesnt hit object. 
      }
     

  }




}
