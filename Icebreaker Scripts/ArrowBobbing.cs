using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBobbing : MonoBehaviour
{
    public float yOffset;
    public float floatStrength = 1; // You can change this in the Unity Editor to 
                                    // change the range of y positions that are possible.


    void Update()
    {
        transform.position = new Vector3(transform.position.x,
            (transform.parent.transform.position.y +yOffset) + ((float)Mathf.Sin(Time.time) * floatStrength),
            transform.position.z);
    }
}
