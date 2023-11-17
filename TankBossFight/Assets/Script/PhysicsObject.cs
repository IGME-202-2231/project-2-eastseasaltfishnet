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
            // 修改这里，检查速度是否不为零
            if (velocity.magnitude > 0.01f)
            {
                ApplyFriction(CoefficientOfFriction);
            }
            //if the object is not moving yet it will call Start moving in the tank control
        }
        
        ApplyVelocity();
        //CheckTopSpeed();
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
        // 更新速度
        velocity += acceleration * Time.deltaTime;
        // 限制速度以避免过快
        velocity = Vector3.ClampMagnitude(velocity, topSpeed);

        // 更新位置
        position += velocity * Time.deltaTime;

        // 更新对象的Transform位置
        transform.position = position;

        // 清除加速度以便下一帧计算新的转向力
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
            // 限制速度
            velocity = velocity.normalized * topSpeed;
            // 重置加速度
            acceleration = Vector3.zero;
        }
        else if (currentSpeedSqr > topSpeedSqr * 0.8f) // 当速度接近最大速度的90%时开始减速
        {
            // 根据与最大速度的差距，逐渐减少加速度
            float reduceFactor = (topSpeedSqr - currentSpeedSqr) / (topSpeedSqr * 0.1f);
            acceleration *= reduceFactor;
        }
    }
}