using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : Agent
{
    public float PlayerDetection;
    public float turningSpeed;
    public float travelSpeed;
    public float stopDistance;


    // Start is called before the first frame update
    private Transform target;
    private Vector3 SeekingForce;




    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    protected override void CalcSteeringForces()
    {
        SeekingForce = EnemySeekPlayer(target.position, travelSpeed, turningSpeed, stopDistance);

        myPhysicsObject.ApplyForce(SeekingForce);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100f);

        // draw all the ray
        Vector3 downwardDirectionOne = Quaternion.Euler(5, 0, 0) * transform.forward;
        Vector3 downwardDirectionTwo = Quaternion.Euler(10, 0, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionOne * 100f);
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionTwo * 100f);

    }
}
