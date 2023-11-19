using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Agent : MonoBehaviour
{

    protected PhysicsObject myPhysicsObject;
    public float maxSpeed = 10f;
    public float maxForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcSteeringForces();
    }

    protected abstract void CalcSteeringForces();

    protected Vector3 MissileSeek(Vector3 targetPos, float turningSpeed)
    {
        Vector3 targetDirection = (targetPos - transform.position).normalized;

        // calculate the front of the missile
        Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, targetDirection) * transform.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);

        Vector3 steeringForce;
        steeringForce = transform.forward * maxSpeed;

        return (steeringForce);
    }
    protected Vector3 MissileAvoidWall( float turningSpeed)
    {
        Vector3 steeringForce = Vector3.zero;

        //The missile will only move upward when advoid the wall
        Vector3 avoidDirection = Vector3.up;

        // rotate at the top of the missile
        Quaternion avoidRotation = Quaternion.FromToRotation(transform.forward, avoidDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, avoidRotation, Time.deltaTime * turningSpeed);

        steeringForce = avoidDirection * maxSpeed;

        return (steeringForce);
    }

    public Vector3 calcFuturePosition(float time = 1f)
    {
        return myPhysicsObject.velocity * time + transform.position;
    }
}
