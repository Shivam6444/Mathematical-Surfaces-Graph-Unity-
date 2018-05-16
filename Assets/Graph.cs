using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;

    public void Awake(){
        Instantiate(pointPrefab);
    }
}
