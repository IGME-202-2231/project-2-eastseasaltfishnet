using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Missile : Agent
{
    public float avoidWallRayDetectionDistance;
    public float turningSpeed;


    // Start is called before the first frame update
    private Transform target;
    private Vector3 SeekingForce;

    private float avoidWallTimer = 0f;
    private float avoidWallDuration = 0.05f;

    private Vector3 downwardDirectionOne;
    private Vector3 downwardDirectionTwo;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTank").transform;
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    protected override void CalcSteeringForces()
    {
        
         downwardDirectionOne = Quaternion.Euler(-5, 0, 0) * transform.forward;
         downwardDirectionTwo = Quaternion.Euler(-10, 0, 0) * transform.forward;

        if (avoidWallTimer > 0)
        {
            avoidWallTimer -= Time.deltaTime;
        }

        //Prevents missiles from jerking while avoiding walls
        //(thinking they've avoided the wall too quickly and then righting themselves)
        //so sent out 3 rays 
        Vector3[] directions = { transform.forward, downwardDirectionOne, downwardDirectionTwo };
        bool wallDetected = false;

        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit , avoidWallRayDetectionDistance) && hit.collider.CompareTag("Wall"))
            {
                SeekingForce = MissileAvoidWall(turningSpeed);
                wallDetected = true;

                //reset the timer so the missile wont seek target 
                avoidWallTimer = avoidWallDuration;
                break;
            }
        }

        if (avoidWallTimer <= 0)
        {
            if (!wallDetected)
            {
                SeekingForce = MissileSeek(target.position, turningSpeed);
            }
        }

        myPhysicsObject.ApplyForce(SeekingForce);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward*100f);

        // draw all the ray
        Vector3 downwardDirectionOne = Quaternion.Euler(5, 0, 0) * transform.forward;
        Vector3 downwardDirectionTwo = Quaternion.Euler(10, 0, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionOne * 100f);
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionTwo * 100f);
    }
}
