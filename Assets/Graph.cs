using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
    public delegate float GraphFunction(float x, float t);
    public Transform pointPrefab;
    Transform[] points;
    [Range(10,100)]public int resolution = 10;
    public GraphFunctionName function;
    static GraphFunction[] functions = { SineFunction, MultiSineFunction };
 
    public void Awake(){

        points = new Transform[resolution];//Just like instansiation of points object.
        float step = 2f / resolution; //Calculation of step according to current resolution.
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 0f;
        position.y = 0f;
        for (int i = 0; i < resolution; i++){
            Transform point = Instantiate(pointPrefab);
            position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update(){
        GraphFunction f = functions[(int)function];
  
        float t = Time.time;//Getting the value of time.
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, t);
            //position.y = MultiSineFunction(position.x,t);//This is our function.
            point.localPosition = position;
        }
    }

    static float SineFunction(float x, float t){
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float MultiSineFunction(float x, float t){
        float y = Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (x + 2f* t));
        y *= 2f / 3f;
        return y;
    }
}
