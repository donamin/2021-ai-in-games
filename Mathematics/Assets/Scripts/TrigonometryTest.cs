using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigonometryTest : MonoBehaviour
{
    public float scale = 0, speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Your code here (Q3): Change the z coordinate of the object such that its path resembles the mathematical infinite symbol!
        transform.position = new Vector3(scale * Mathf.Sin(speed * Time.time), transform.position.y, transform.position.z);
    }
}
