using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    public Transform pointPrefab;
    [Range(10,100)]public int resolution = 10;

    public void Awake(){
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        
        position.z = 0f;
        for (int i = 0; i < resolution; i++ ){

            Transform point = Instantiate(pointPrefab);
            //  point.localPosition = Vector3.right *((i + 0.5f) /5f - 1f);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x;//THIS IS FUNCTION
            point.localPosition = position;
            point.localScale = scale;
        }


    }
}
