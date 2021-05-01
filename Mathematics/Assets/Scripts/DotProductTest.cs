using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductTest : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 25, Color.green);
        Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
        Vector3 a = transform.forward;
        Vector3 b = enemy.transform.position - transform.position;
        float dot = a.x * b.x + a.z * b.z;
        //print(dot);
        float angle = Mathf.Acos(dot / (a.magnitude * b.magnitude)) * Mathf.Rad2Deg;
        if (Vector3.Dot(b, transform.right) < 0)
            angle *= -1;
        //print(angle);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 25, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.up * 25, Color.red);
        Vector3 right = -Vector3.Cross(transform.forward, transform.up);
        //Vector3 right = Vector3.Cross(transform.up, transform.forward);
        Debug.DrawLine(transform.position, transform.position + right * 25, Color.blue);

        //TODO: Your code here (Q2): Write an if statement for printing an attack message when
        //the (absolute value) of the angle is less than 45 degrees and
        //the distance to the enemy is less than 40
    }
}