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

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed*10f);

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

    protected Vector3 MissileSeparation(float separationRadius, float separationStrength)
    {
        Vector3 separationForce = Vector3.zero;
        int nearbyMissilesCount = 0;

        foreach (GameObject missile in AgentManger.Instance.allMissiles)
        {
            if (missile != this.gameObject) 
            {
                float distance = Vector3.Distance(transform.position, missile.transform.position);

                if (distance < separationRadius) 
                {
                    Vector3 directionToAvoid = transform.position - missile.transform.position;
                    separationForce += directionToAvoid.normalized / distance; 
                    nearbyMissilesCount++;
                }
            }
        }

        if (nearbyMissilesCount > 0)
        {
            separationForce /= nearbyMissilesCount; 
            separationForce = separationForce.normalized * separationStrength; 
        }


        Debug.Log  ("The sepreation force of missile" + separationForce);



        return separationForce;
    }
    protected Vector3 HpBoxWander(float hpBoxSpeed, float wanderRadius, float wanderWeight)
    {
        Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
        wanderTarget.y = transform.position.y;

        Vector3 wanderDirection = Random.insideUnitSphere.normalized;

        wanderDirection.y = 0;
        wanderTarget += wanderDirection * wanderRadius * wanderWeight;

        
        if (Vector3.Distance(transform.position, wanderTarget) > wanderRadius)
        {
            wanderTarget = transform.position + (wanderTarget - transform.position).normalized * wanderRadius;
        }

        
        return HpBoxSeek(wanderTarget, hpBoxSpeed, 1.0f); 
    }

    protected Vector3 HpBoxSeek(Vector3 targetPosition, float hpBoxSpeed, float weight)
    {
        
        Vector3 desiredVelocity = (targetPosition - transform.position).normalized * hpBoxSpeed;
        Vector3 steeringForce = (desiredVelocity - myPhysicsObject.velocity) * weight;
        return steeringForce;
    }

    protected Vector3 HpBoxSepreation(float separationRadius,float separationStrength)
    {
        Vector3 separationForce = Vector3.zero;
        int nearbyHpBoxes = 0;

        foreach (GameObject hpBox in AgentManger.Instance.allHpBox)
        {
            if (hpBox != this.gameObject) 
            {
                float distance = Vector3.Distance(transform.position, hpBox.transform.position);

                if (distance < separationRadius) 
                {
                    Vector3 directionToAvoid = transform.position - hpBox.transform.position;
                    separationForce += directionToAvoid.normalized / distance; 
                    nearbyHpBoxes++;
                }
            }
        }

        if (nearbyHpBoxes > 0)
        {
            separationForce /= nearbyHpBoxes; 
            separationForce = separationForce.normalized * separationStrength; 
        }

        return separationForce;

    }
    protected Vector3 keepHpBoxInBound(float hpBoxSpeed, float hpBoxTurnSpeed, float weight)
    {
        Vector3 force = Vector3.zero;

        float  boundaryPadding = 5f;

        Vector3 boundaryMin = new Vector3(5.27f + boundaryPadding, 0f, -86.3f + boundaryPadding);
        Vector3 boundaryMax = new Vector3(120.9f - boundaryPadding, 0f, -5.97f - boundaryPadding);


        if (transform.position.x <= boundaryMin.x ||
            transform.position.x >= boundaryMax.x ||
            transform.position.z <= boundaryMin.z ||
            transform.position.z >= boundaryMax.z)
        {
            Vector3 centerPoint = new Vector3((boundaryMin.x + boundaryMax.x) / 2, transform.position.y, (boundaryMin.z + boundaryMax.z) / 2);
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToCenter);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * hpBoxTurnSpeed);

            force = directionToCenter * hpBoxSpeed * weight;
        }

        return force;
    }
    /// <summary>
    /// for the hp box to get around the obstacle
    /// </summary>
    /// <param name="avoidDistance"></param>
    /// <param name="obstacles"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    protected Vector3 HpBoxAvoidObstacle(float avoidDistance, List<GameObject> obstacles,float weight)
    {
        Vector3 avoidanceForce = Vector3.zero;
        int obstaclesAvoided = 0;

        
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle != null)
            {
                Vector3 obstaclePosition = obstacle.transform.position;
                Vector3 toObstacle = obstaclePosition - transform.position;
                float distanceToObstacle = toObstacle.magnitude;

               
                if (distanceToObstacle < avoidDistance && Vector3.Dot(toObstacle, transform.forward) > 0)
                {
                    
                    avoidanceForce += (transform.position - obstaclePosition).normalized / distanceToObstacle;
                    obstaclesAvoided++;
                }
            }
        }

        
        if (obstaclesAvoided > 0)
        {
            avoidanceForce /= obstaclesAvoided;
            avoidanceForce *= maxSpeed* weight;
            avoidanceForce.y = 0;
        }

        return avoidanceForce;
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
    protected Vector3 AvoidObstacleForTank(float enemySpeed,float turningSpeed, float avoidDistance, List<GameObject> obstacles,Vector3 playerTankPosition)
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

                
                Vector3 rightPos = transform.position + transform.right * avoidDistance;
                Vector3 leftPos = transform.position - transform.right * avoidDistance;


                float distanceToRight = Vector3.Distance(rightPos, obstacle.transform.position);
                float distanceToLeft = Vector3.Distance(leftPos, obstacle.transform.position);

                //determine the tuen direction
                Vector3 bestDirection;
                if (distanceToRight > distanceToLeft)
                {
                    bestDirection = transform.right;
                }
                else
                {
                    bestDirection = -transform.right;
                }

                Quaternion targetRotation = Quaternion.LookRotation(bestDirection);
                float dynamicTurningSpeed = turningSpeed * Mathf.Clamp01((avoidDistance - distance) / avoidDistance); 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * dynamicTurningSpeed);

                
                float dynamicSpeedFactor = Mathf.Clamp01((avoidDistance - distance) / avoidDistance); 
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

