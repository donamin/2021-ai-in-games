using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMatrix : MonoBehaviour
{
    public float theta = 0;
    public GameObject rotatedObject;
    //TODO: Your code here (Q1): Create the second object here.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(theta * Mathf.Deg2Rad);
        float cos = Mathf.Cos(theta * Mathf.Deg2Rad);
        float newX = transform.position.x * cos - transform.position.z * sin;
        float newZ = transform.position.x * sin + transform.position.z * cos;
        rotatedObject.transform.position = new Vector3(newX, transform.position.y, newZ);

        //TODO: Your code here (Q1): Rotate the second object such that it is rotated by -theta degrees (the code will be similar to the code above!)
    }
}