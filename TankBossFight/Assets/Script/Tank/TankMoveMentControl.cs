using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


public class TankControl : MonoBehaviour
{
    [SerializeField]
    float hullSpeed = 5f;
    [SerializeField]
    float hullRotateSpeed = 5f;

    [SerializeField]
    float turretRotateSpeed = 5f;
    



    public GameObject turret;


    private Camera followCamera;
    private Vector3 movementInput;

    //use to keep the camera fallow the tank
    private Vector3 offsetOfTankAndCamera;

    private float turretX;

    // Start is called before the first frame update
    void Start()
    {
        //get the current distance from the camera to the tank
        offsetOfTankAndCamera = turret.transform.position - Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TankHullMove();
        TankHullRotate();
        TurretRotation();
        CameraFallowTankMoveAndRotate();
    }

    /// <summary>
    /// move the tank only move fowrd and back 
    /// </summary>
    public void TankHullMove()
    {
        transform.Translate(0, 0, movementInput.y * hullSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Tanks can only turn in place.
    /// </summary>
    public void TankHullRotate()
    {
        transform.Rotate(0, movementInput.x * hullRotateSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// will fallow mouse move on x axis
    /// </summary>
    public void TurretRotation()
    {
        turret.transform.Rotate(Vector3.up, turretX * turretRotateSpeed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnMoveForTurretRotation(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<Vector2>();
        turretX = ClampValue(delta.x);
        
    }
    public float ClampValue(float delta)
    {
        if (delta <= -1)
        {
            return -1;
        }
        else if (delta >= 1)
        {
            return 1;
        }
        else
            return delta;
    }

    /// <summary>
    /// keep the camera fallow the tank
    /// </summary>
    public void CameraFallowTankMoveAndRotate()
    {
        //dont need the move code becasue it have hull as it parent 

        //rotate with the turret
        Camera.main.transform.rotation = turret.transform.rotation;
    }
}
