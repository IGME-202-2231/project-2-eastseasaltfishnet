using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Agent
{
    // Start is called before the first frame update
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTank").transform;
        myPhysicsObject = GetComponent<PhysicsObject>();
    }

    protected override void CalcSteeringForces()
    {
        
        MissileSeek(target.position, 5f);

    }
}
