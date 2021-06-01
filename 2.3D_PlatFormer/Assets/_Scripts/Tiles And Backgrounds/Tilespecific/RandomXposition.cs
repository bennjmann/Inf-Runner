using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomXPosition : MonoBehaviour {
    public List<GameObject> m_Object;
    private void Start() {
        foreach (GameObject trans in m_Object) {
            float random = Random.Range(-12.4f, 11.4f);
            Vector3 transformPosition = trans.transform.localPosition;
            transformPosition.x = random;
            Debug.Log(transformPosition);
        }
        
    }



}

