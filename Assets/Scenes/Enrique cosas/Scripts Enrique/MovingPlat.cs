using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    public Transform platform;

    public Transform starttransform;

    public Transform endtransform;

    public Transform destination;

    public float platformspeed;
    

    Vector3 direction;

    private void FixedUpdate()
    {
        platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * platformspeed * Time.fixedDeltaTime);


        if (Vector3.Distance(platform.position, destination.position) < platformspeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == starttransform ? endtransform : starttransform);
        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;


        Gizmos.DrawWireCube(starttransform.position, platform.localScale);


        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endtransform.position, platform.localScale);

        
    }

        void SetDestination(Transform dest)
        {
            destination = dest;
            direction = (destination.position - platform.position).normalized;

        }

    }
