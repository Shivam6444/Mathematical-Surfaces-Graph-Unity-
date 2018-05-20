using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {

    const float pi = Mathf.PI;
    public delegate Vector3 GraphFunction(float u, float v, float t);
  
    public Transform pointPrefab;
    Transform[] points;
    [Range(10,100)]public int resolution = 10;
    public GraphFunctionName function;
    static GraphFunction[] functions = { SineFunction, MultiSineFunction, Sine2DFunction, MultiSine2DFunction,
        Ripple, Cylinder, Sphere};
 
    public void Awake(){
        points = new Transform[resolution * resolution];//Just like instansiation of points object.
        float step = 2f / resolution; //Calculation of step according to current resolution.
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.z = 0f;
        position.y = 0f;
        for (int i = 0; i < points.Length; i++){
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    void Update(){
        GraphFunction f = functions[(int)function];
        float t = Time.time;//Getting the value of time from Time Class.
        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++){
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++){
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }


    static Vector3 Ripple(float x, float z, float t){
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }


    static Vector3 MultiSine2DFunction(float x, float z, float t){
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t / 2f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t){
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 SineFunction(float x, float z, float t){
        Vector3 p;
        p.x = x;//X position.
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t){
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t){
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }
    static Vector3 Sphere(float u, float v, float t){
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);
        p.x = s * Mathf.Sin(pi * u);
        p.y = r * Mathf.Sin(pi * 0.5f * v); ;
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }
}
