using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 direction;
    public Vector3 position;
    public Vector3 acceleration = Vector3.zero;
    public float mass;
    public float CoefficientOfFriction;
    public float gravityStrength;
    public float topSpeed;
    public float clampSpeed;

    public bool useGravity;
    public bool useFriction;

    
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (useGravity)
        {
            ApplyGravity();
        }

        if (useFriction)
        {
            
            if (velocity.magnitude > 0.01f)
            {
                ApplyFriction(CoefficientOfFriction);
            }
            //if the object is not moving yet it will call Start moving in the tank control
        }
        ApplyVelocity();
       
        CheckTopSpeed();
        
    }

    public void ApplyGravity()
    {
        acceleration += new Vector3(0, -gravityStrength, 0);
    }
    public void ApplyForce(Vector3 force)
    {
        
        acceleration += force / mass;
        

    }
    public void ApplyFriction(float coeff)
    {


        if (velocity.magnitude < 1.5f)
        {
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
            return;
        }
        // calculate the friction 
        Vector3 friction = -velocity.normalized * coeff;
        float maxFrictionMagnitude = velocity.magnitude / Time.deltaTime;

        // prevent the friction from acting back 
        if (friction.magnitude > maxFrictionMagnitude)
        {
            friction = friction.normalized * maxFrictionMagnitude;
        }

        //Vector3 friction = velocity * -1;
        //friction.Normalize();
        //friction = friction * coeff;
        ApplyForce(friction);
    }


    public void ApplyVelocity()
    {
        velocity += acceleration * Time.deltaTime;
        //prevent the tank from mobing too fast
        velocity = Vector3.ClampMagnitude(velocity, clampSpeed);


        position += velocity * Time.deltaTime;


        transform.position = position;
        //prevent some werid issue
        acceleration = Vector3.zero;


    }
    public void StartMoveing(float _direction)
    {
        // Calculate the velocity for this frame - New
        if (_direction > 0)
        {
            velocity = transform.forward * 3f; 
        }
        else
        {
            velocity = transform.forward  * -3f; 
        }
    }
    public void CheckTopSpeed()
    {
        float currentSpeedSqr = velocity.sqrMagnitude;
        float topSpeedSqr = topSpeed * topSpeed;


        
        if (currentSpeedSqr > topSpeedSqr)
        {
            velocity = velocity.normalized * topSpeed;
            
            acceleration = Vector3.zero;
        }
        // when the speed almost reach 80% of the top speed, the slow down the increase of acceleration 
        else if (currentSpeedSqr > topSpeedSqr * 0.8f) 
        {
            float reduceFactor = (topSpeedSqr - currentSpeedSqr) / (topSpeedSqr * 0.1f);
            acceleration *= reduceFactor;
        }
        
    }
}