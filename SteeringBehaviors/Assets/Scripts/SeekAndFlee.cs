using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndFlee : BaseSteeringBehavior
{
    public GameObject target;
    public bool flee;
    public float maxAcceleration = 2.5f;

    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering;
        steering.linear = Vector3.zero;
        steering.angular = 0;

        if(target)
        {
            if(flee)
                steering.linear = transform.position - target.transform.position;
            else
                steering.linear = target.transform.position - transform.position;

            steering.linear.y = 0;
            steering.linear.Normalize();
            steering.linear *= maxAcceleration;

            steering.angular = 0;
        }

        return steering;
    }
}