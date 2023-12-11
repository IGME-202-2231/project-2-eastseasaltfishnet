using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeatShell : MonoBehaviour
{

    
    public float speed ;
    public string targetTag;

    private PhysicsObject myPhysicsObject;
    private void Start()
    {
        myPhysicsObject = GetComponent<PhysicsObject>();
        myPhysicsObject.velocity = transform.forward * speed;
    }

    

}
