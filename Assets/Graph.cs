using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;

    public void Awake(){
        
        for(int i = 0; i < 10; i++ ){
            Transform point = Instantiate(pointPrefab);
            point.localPosition = Vector3.right *i;
           
        }


    }
}
