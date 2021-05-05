using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematics : MonoBehaviour
{
    BaseSteeringBehavior steeringBehavior;

    public Vector3 velocity;
    public float rotation;

    // Start is called before the first frame update
    void Start()
    {
        steeringBehavior = GetComponent<BaseSteeringBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        if(velocity.sqrMagnitude > 0.001f)
        {
            transform.forward = velocity;
        }

        if(steeringBehavior)
        {
            SteeringOutput steering = steeringBehavior.GetSteering();
            velocity += steering.linear * Time.deltaTime;
            rotation += steering.angular * Time.deltaTime;
        }

        if(transform.position.magnitude > 40)
        {
            transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            transform.Rotate(Vector3.up, Random.Range(0.0f, 360.0f));

            velocity = Vector3.zero;
            rotation = 0;
        }
    }
}