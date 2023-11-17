using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Agent
{
    // Start is called before the first frame update
    private Transform target;
    private Vector3 SeekingForce;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTank").transform;
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    protected override void CalcSteeringForces()
    {

        SeekingForce = MissileSeek(target.position, 5f);

        // Ӧ������������������
        myPhysicsObject.ApplyForce(SeekingForce);

    }
}
