using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackGroundLoop : MonoBehaviour {

  public float InitialSpawningOffset = -10f;        // Offset of z position from 0 World space.
  public float PlayerOffset = 50f;          // Offset From Players z Position

  public GameObject m_LevelStart;           // Transform of LevelManager.
  public GameObject[] m_BackGround;        // Backgrounds.
  public GameObject m_Player;             // Player gameobject.
  
  
  private int m_Random;                    
  int m_Children = 5;                     // Number Of Children to Spawn.
  private float m_NextSpawnPosition;        //  Previous background spawned width
  private int m_BackgroundsSpawned;     // How many background has been spawned.
  

  private void Start() {
    for (int i = 0; i <= m_Children; i++) {
      m_Random = Random.Range(0, m_BackGround.Length);
      SpawnBackGrounds(m_BackGround[m_Random]);
      m_BackgroundsSpawned++;
    }
  }

  
  /// <summary>
  /// Spawns Number("children") of Backgrounds.
  /// Sets Position Next to each other to Tile Backgrounds.
  /// First Background Spawns At Z offset Of "InitialSpawningOffset".
  /// </summary>
  /// <param name="obj"></param>
  private void SpawnBackGrounds(GameObject obj) {
    float currentExtentsZ;
    if (m_BackgroundsSpawned == 0) {                                                      // Checks if First Tile Or not.                                  
      currentExtentsZ = InitialSpawningOffset;                        
    } else currentExtentsZ = obj.GetComponent<SpriteRenderer>().bounds.extents.z;        // If not first tile Get the extent.z of Obj(Next To Spawn Background).
    m_NextSpawnPosition += currentExtentsZ;
    GameObject clone = Instantiate(obj) as GameObject;                 // Original obj.
    GameObject c = Instantiate(clone) as GameObject;                  // Clone of Obj to child it.
      c.GetComponent<Renderer>().enabled = true;                     // Renderer For the Original is Disabled, This enables it.
      c.transform.SetParent(m_LevelStart.transform);                                                                         // sets The original obj as parent 
      c.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y,m_NextSpawnPosition);    // spawn Background at original.x, original.y, m_NextSpawnPosition.
      m_NextSpawnPosition = c.transform.position.z + c.GetComponent<SpriteRenderer>().bounds.extents.z;               // Sets m_NextSpawnPosition to the .z pos + extents.z
      c.name = obj.name + 1;  // Sets child name to numbers obj 1, 2, 3, etc.. 
    
  }
  /// <summary>
  /// Changes children From Last to first, first to last Depending on players Position + Players offset. 
  /// </summary>
  /// <param name="obj"></param>
  void RepositionChildObjects(GameObject obj) {
    Transform[] children = obj.GetComponentsInChildren<Transform>();        // Gets the children's transforms
    if (children.Length > 1) {
      GameObject firstChild = children[1].gameObject;                      // Get the First Child
      GameObject lastChild = children[children.Length - 1].gameObject;    // Gets the last Child
      float lasteExtentsZ = lastChild.GetComponent<SpriteRenderer>().bounds.extents.z;           
      float firstExtentsZ = firstChild.GetComponent<SpriteRenderer>().bounds.extents.z;
      float lastObjectsPosition = lastChild.transform.position.z + lasteExtentsZ;
      if (m_Player.transform.position.z + PlayerOffset  > firstChild.transform.position.z + firstExtentsZ) {  // if Player has pass First Child Set it To And as last Position + Offset.
        firstChild.transform.position = new Vector3(lastChild.transform.position.x, lastChild.transform.position.y, lastObjectsPosition += firstExtentsZ);
        firstChild.transform.SetAsLastSibling();
      } else if (m_Player.transform.position.z - PlayerOffset > lastChild.transform.position.z - lasteExtentsZ) { // last child if player passes Object/Bacground Set as First.
        lastChild.transform.SetAsFirstSibling();
      }
      

    }
  }
  private void LateUpdate() {
    for (int i = 0; i <= m_Children; i++) {
      RepositionChildObjects(m_LevelStart);    // <== Foreach child 
    }
  }
}