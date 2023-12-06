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

    public float obstacleAvoidanceDistance; // ’œ∞≠ŒÔ∂„±‹æ‡¿Î

    [SerializeField]
    float advoidWeight = 1f;

    // Start is called before the first frame update
    private List<GameObject> walls;
    private Transform target;
    private Vector3 totalForce;
    private Vector3 tempForce;



    private void Start()
    {
        tempForce = Vector3.zero;
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;

        GameObject[] wallsArray = GameObject.FindGameObjectsWithTag("obstacle");
        walls = new List<GameObject>(wallsArray);
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    protected override void CalcSteeringForces()
    {
        totalForce = Vector3.zero;
        Vector3 avoidObstacleForce = AvoidObstacle(hullPower,turningSpeed, obstacleAvoidanceDistance, walls, target.position);

        if (avoidObstacleForce.magnitude < 3.5f&& turnTime! < 0) 
        {
            totalForce += EnemySeekPlayer(target.position, hullPower, turningSpeed, stopDistance, maxAcceleration);
        }
        //keep move foward while turning 
        else
        {
            totalForce += transform.forward * hullPower * 100f;
        }

        Debug.Log(turnTime);
        turnTime -= Time.deltaTime;
        myPhysicsObject.ApplyForce(totalForce);
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
