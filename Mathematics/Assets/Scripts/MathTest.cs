using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, transform.position);
        float length = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) + Mathf.Pow(transform.position.z, 2));
        //Vector3.Distance(Vector3.zero, transform.position);
        //transform.position.magnitude;
        //transform.position.sqrMagnitude;
        Debug.Log(length);
        //if(transform.position.magnitude < 2)
        if (transform.position.sqrMagnitude < 4)
        {
        }
    }
}