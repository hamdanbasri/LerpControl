using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
public class LerpControl: MonoBehaviour 
{  
    [Header("If Get Start Position is enabled, it will overwrite any value that you set")]
    [Header("Conditions")]
    public bool getStartPosition;
    public bool repeatable = false;
    
    [Header("Copy axis value from the Start Position")]
    [Header("Axis")]    
    public bool copyX;
    public bool copyY;
    public bool copyZ;

    [Space(10f)]
    [Header("Position Set")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    [Space(10f)]
    [Header("Speed and Duration")] 
    public float speed = 1.0f;  
    public float duration = 1.0f;  
    float startTime, totalDistance;     
      
    IEnumerator Start() 
    {  
        startTime = Time.time;

        if(getStartPosition)
        {
            startPosition = transform.position;
        }

        if(copyX)
        {
            endPosition.x = startPosition.x;
        }

        if(copyY)
        {
            endPosition.y = startPosition.y;
        }

        if(copyZ)
        {
            endPosition.z = startPosition.z;
        }

        //Calculate the distance between the startPosition and endPosition  
        totalDistance = Vector3.Distance(startPosition, endPosition);  
        while (repeatable) {  
            yield  
            return Repeat(startPosition, endPosition, duration);  
            yield  
            return Repeat(endPosition, startPosition, duration);  
        }  
    }     

    void Update() 
    {  
        if (!repeatable) 
        {  
            float currentDuration = (Time.time - startTime) * speed;  
            float journey = currentDuration / totalDistance;  
            this.transform.position = Vector3.Lerp(startPosition, endPosition, journey);  
        }  
    }  

    public IEnumerator Repeat(Vector3 a, Vector3 b, float time) 
    {  
        float i = 0.0f;  
        float rate = (1.0f / time) * speed;  
        while (i < 1.0f) 
        {  
            i += Time.deltaTime * rate;  
            this.transform.position = Vector3.Lerp(a, b, i);  
            yield  
            return null;  
        }  
    }  
}
