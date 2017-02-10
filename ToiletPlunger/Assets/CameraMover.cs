using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    // Use this for initialization
    public Transform startMarker;
    public Transform endMarker;
    
    public bool above = false;
    void Start()
    {
       
    }
    void Update()
    {
        if (above)
        {
            transform.position = endMarker.position;
            transform.rotation = endMarker.rotation;
        }
        if (!above)
        {
            transform.position = startMarker.position;
            transform.rotation = startMarker.rotation;
        }
       
    }
}
