using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMove : Agent
{
    public float PlayerDetection;
    public float turningSpeed;
    public float hullPower;
    public float stopDistance;
    public float maxAcceleration;

    public float obstacleAvoidanceDistance; 

    

    [SerializeField]
    float advoidWeight = 1f;

    // Start is called before the first frame update
    private List<GameObject> walls;
    private Transform target;
    private Vector3 totalForce;
    private Vector3 tempForce;
    private Vector3 boundaryMin;
    private Vector3 boundaryMax;


    private void Start()
    {
        tempForce = Vector3.zero;
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;

        GameObject[] wallsArray = GameObject.FindGameObjectsWithTag("obstacle");
        walls = new List<GameObject>(wallsArray);
        myPhysicsObject = GetComponent<PhysicsObject>();

        float boundaryPadding = 5f; 

        boundaryMin = new Vector3(5.27f + boundaryPadding, 0f, -86.3f + boundaryPadding);
        boundaryMax = new Vector3(120.9f - boundaryPadding, 0f, -5.97f - boundaryPadding);
    }

    protected override void CalcSteeringForces()
    {
        totalForce = Vector3.zero;
        Vector3 avoidObstacleForce = AvoidObstacle(hullPower,turningSpeed, obstacleAvoidanceDistance, walls, target.position);

        //if the enemy is not in bound seek player 
        if (!IsWithinBoundary(transform.position))
        {
            totalForce += EnemySeekPlayer(target.position, hullPower, turningSpeed, stopDistance, maxAcceleration);
        }
        //if player is in the bound then do the obsticle advoid 
        else
        {
            if (avoidObstacleForce.magnitude < 3.5f && turnTime! < 0)
            {
                totalForce += EnemySeekPlayer(target.position, hullPower, turningSpeed, stopDistance, maxAcceleration);
            }
            //keep move foward while turning 
            else
            {
                totalForce += transform.forward * hullPower * 100f;
            }
        }

        turnTime -= Time.deltaTime;
        myPhysicsObject.ApplyForce(totalForce);
    }

    private bool IsWithinBoundary(Vector3 position)
    {
        bool isWithinXBoundary = position.x >= boundaryMin.x && position.x <= boundaryMax.x;
        bool isWithinZBoundary = position.z >= boundaryMin.z && position.z <= boundaryMax.z;

        return isWithinXBoundary && isWithinZBoundary;
    }


    void OnDrawGizmos()
    {
        //obstacle

        Vector3 boxSize = new Vector3(10f,
              10f, obstacleAvoidanceDistance);

        Vector3 boxCenter = Vector3.zero;
        boxCenter.z += obstacleAvoidanceDistance / 2f;

        Gizmos.color = Color.green;

        Gizmos.matrix = transform.localToWorldMatrix; //set the gimoz to be creted at with coordinate of the object
        Gizmos.DrawWireCube(boxCenter, boxSize);
        Gizmos.matrix = Matrix4x4.identity;


        //
        //  Draw lines to found obstacles
        //
        Gizmos.color = Color.red;

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {

            Gizmos.DrawLine(transform.position, wall.transform.position);
        }

    }
}
