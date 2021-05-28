using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    // Kills self after 60 seconds 
    private void Update() {
        StartCoroutine(KilSelf());
    }
    private IEnumerator KilSelf() {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
