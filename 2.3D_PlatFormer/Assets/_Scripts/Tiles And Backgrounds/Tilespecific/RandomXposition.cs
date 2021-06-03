using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomXPosition : MonoBehaviour {
    public GameObject[] m_Object;
    private void Start() {
        int numberOfObj = 0;                                                                // cycle through array.
        foreach (GameObject GameObj in m_Object) {                                          // ForEach object in the array set to random x position.
            float random = Random.Range(-11.25f, 11.4f);
            m_Object[numberOfObj].transform.position = new Vector3(random, m_Object[numberOfObj].transform.position.y, m_Object[numberOfObj].transform.position.z);
            // Debug.Log(m_Object[numberOfObj].transform.position);
            numberOfObj++;
        }
    }
}

