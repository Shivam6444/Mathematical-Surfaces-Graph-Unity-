using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    const float pi = Mathf.PI;
    public delegate float GraphFunction(float x, float z, float t);
    public Transform pointPrefab;
    Transform[] points;
    [Range(10,100)]public int resolution = 10;
    public GraphFunctionName function;
    static GraphFunction[] functions = { SineFunction, MultiSineFunction, Sine2DFunction, MultiSine2DFunction};
 
    public void Awake(){

        points = new Transform[resolution * resolution];//Just like instansiation of points object.
        float step = 2f / resolution; //Calculation of step according to current resolution.
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 0f;
        position.y = 0f;

        for (int i = 0, z = 0; z < resolution; z++){

            position.z = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++){
                Transform point = Instantiate(pointPrefab);
                position.x = (x + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }
    }

    void Update(){
        GraphFunction f = functions[(int)function];
  
        float t = Time.time;//Getting the value of time.
        for(int i = 0; i < points.Length; i++){
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = f(position.x, position.z, t);
            //position.y = MultiSineFunction(position.x,t);//This is our function.
            point.localPosition = position;
        }
    }
    static float MultiSine2DFunction(float x, float z, float t){
        float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
        y += Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        return y;
    }

    static float Sine2DFunction(float x, float z, float t){
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *= 0.5f;
        return y;
    }

    static float SineFunction(float x, float z, float t){
        return Mathf.Sin(pi * (x + t));
    }

    static float MultiSineFunction(float x, float z, float t){
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + 2f* t));
        y *= 2f / 3f;
        return y;
    }
}
