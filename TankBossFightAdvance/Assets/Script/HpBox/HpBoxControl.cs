using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoxControl : Agent
{


    public float hpBoxSpeed ;
    public float hpBoxTurnSpeed ;
    public float wanderDistance ;
    public float wanderAngle;
    public float wanderWeight;
    public float sperateDistance;
    public float separationStrength;
    public float avoidWallDistance;

    private Vector3 initialDirection;
    private List<GameObject> obstacles;
    void Start()
    {
        GameObject[] obstacleArray = GameObject.FindGameObjectsWithTag("obstacle");
        obstacles= new List<GameObject>(obstacleArray);

        myPhysicsObject = GetComponent<PhysicsObject>();
        myPhysicsObject = GetComponent<PhysicsObject>();


        initialDirection = Random.onUnitSphere;
        initialDirection.y = 0; 

        myPhysicsObject.ApplyForce(initialDirection * hpBoxSpeed);
    }

    protected override void CalcSteeringForces()
    {
        Vector3 steeringForce = Vector3.zero;


        steeringForce += HpBoxWander(hpBoxSpeed, wanderDistance, wanderWeight);


        steeringForce += keepHpBoxInBound(hpBoxSpeed, hpBoxTurnSpeed, wanderWeight);

        steeringForce += HpBoxSepreation(sperateDistance,separationStrength);

        steeringForce += HpBoxAvoidObstacle(avoidWallDistance, obstacles,3f);

        myPhysicsObject.ApplyForce(steeringForce);
    }
}
