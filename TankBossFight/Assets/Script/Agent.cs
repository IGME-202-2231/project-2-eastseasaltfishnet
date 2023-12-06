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

    protected float turnTime = 0f;

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
    protected Vector3 MissileAvoidWall(float turningSpeed)
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

    protected Vector3 EnemySeekPlayer(Vector3 targetPos, float enemySpeed, float enemyTurnSpeed, float stopDistance, float maxAcceleration)
    {


        Vector3 flatTargetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        Vector3 targetDirection = (flatTargetPos - transform.position).normalized;


        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion slerpedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * enemyTurnSpeed);
        transform.rotation = Quaternion.Euler(0, slerpedRotation.eulerAngles.y, 0);

        float angleToTarget = Quaternion.Angle(transform.rotation, targetRotation);

        float distance = Vector3.Distance(targetPos, transform.position);

        if (angleToTarget < 5.0f && distance > stopDistance) 
        {

            Vector3 force = transform.forward * enemySpeed*100f;
            return force;
        }
        else
        {
            myPhysicsObject.acceleration = Vector3.zero;
            return Vector3.zero;
        }
    }

    /// <summary>
    /// this method wont actually move the player with the force the forc that it return just help with determine the advoid speed of the enemy 
    /// </summary>
    /// <param name="enemySpeed"></param>
    /// <param name="turningSpeed"></param>
    /// <param name="avoidDistance"></param>
    /// <param name="obstacles"></param>
    /// <returns></returns>
    protected Vector3 AvoidObstacle(float enemySpeed,float turningSpeed, float avoidDistance, List<GameObject> obstacles,Vector3 playerTankPosition)
    {
        Vector3 steeringForce = Vector3.zero;

        foreach (GameObject obstacle in obstacles)
        {
            Vector3 distanceVector = obstacle.transform.position - transform.position;
            float distance = distanceVector.magnitude;

            Vector3 distanceVectorToObstacle = obstacle.transform.position - transform.position;
            Vector3 distanceVectorToPlayer = playerTankPosition - transform.position;

            float distanceToObstacle = distanceVectorToObstacle.magnitude;
            float distanceToPlayer = distanceVectorToPlayer.magnitude;

            // if player is closer jump to next obsticle 
            if (distanceToPlayer < distanceToObstacle)
            {
                continue;
            }
            if (distance < avoidDistance && Vector3.Dot(distanceVector, transform.forward) > 0)
            {
                //get a turn time to advoid the obsticle 
                CalculateAvoidanceCooldown(obstacle.transform.position, turningSpeed, maxSpeed);

                // 计算左右转向的目标点
                Vector3 rightPos = transform.position + transform.right * avoidDistance;
                Vector3 leftPos = transform.position - transform.right * avoidDistance;

                // 计算到左右两边的距离
                float distanceToRight = Vector3.Distance(rightPos, obstacle.transform.position);
                float distanceToLeft = Vector3.Distance(leftPos, obstacle.transform.position);

                // 选择转向方向
                Vector3 bestDirection;
                if (distanceToRight > distanceToLeft)
                {
                    bestDirection = transform.right;
                }
                else
                {
                    bestDirection = -transform.right;
                }

                // 应用旋转
                Quaternion targetRotation = Quaternion.LookRotation(bestDirection);
                float dynamicTurningSpeed = turningSpeed * Mathf.Clamp01((avoidDistance - distance) / avoidDistance); // 距离越近，转向越快
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * dynamicTurningSpeed);

                // 应用转向力
                float dynamicSpeedFactor = Mathf.Clamp01((avoidDistance - distance) / avoidDistance); // 距离越近，速度系数越大
                steeringForce += bestDirection.normalized * maxSpeed * dynamicSpeedFactor * 100f;
            }
        }
        return steeringForce;
    }
    /// <summary>
    /// guess the time take to advoid the wall
    /// </summary>
    /// <param name="obstaclePosition"></param>
    /// <param name="turningSpeed"></param>
    /// <param name="topSpeed"></param>
    protected void CalculateAvoidanceCooldown(Vector3 obstaclePosition, float turningSpeed,float topSpeed)
    {
        float distanceToObstacle = Vector3.Distance(transform.position, obstaclePosition);

        turnTime = distanceToObstacle / topSpeed/2f ;
    }
   
}

