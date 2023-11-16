using System.Collections;
using System.Collections.Generic;
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

    protected void MissileSeek(Vector3 targetPos, float weight)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = (desiredVelocity - myPhysicsObject.velocity) * weight;


        //Vector3 steeringForce=Vector3.zero;
        //make sure the force is not greater than max force
        seekingForce = Vector3.ClampMagnitude(seekingForce, maxForce);

        Debug.Log(seekingForce);
        myPhysicsObject.ApplyForce(seekingForce);
    }
}
