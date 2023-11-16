using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 direction;
    public Vector3 position;
    public float mass;
    public float CoefficientOfFriction;
    public float gravityStrength;

    private Vector3 acceleration = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        ApplyFriction(CoefficientOfFriction);
        ApplyVelocity();
    }

    public void ApplyGravity()
    {
        acceleration += new Vector3(0, -gravityStrength, 0);
    }
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
    void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        ApplyForce(friction);
    }


    public void ApplyVelocity()
    {
        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;


        // Grab current direction from velocity  - New
        direction = velocity.normalized;

        transform.position = position;
    }
}