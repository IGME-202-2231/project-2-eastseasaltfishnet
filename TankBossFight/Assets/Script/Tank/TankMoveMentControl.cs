using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


public class TankControl : MonoBehaviour
{
    [SerializeField]
    float hullPower = 5f;
    float maxHullForce = 10f;
    [SerializeField]
    float hullRotateSpeed = 5f;

    [SerializeField]
    float turretRotateSpeed = 5f;


    protected PhysicsObject myPhysicsObject;

    public GameObject turret;

    private Vector3 movementInput;
    private Vector3 rotateInput;

    //use to keep the camera fallow the tank
    private Vector3 offsetOfTankAndCamera;
    private Quaternion rotationOfTankAndCamera;

    private Vector3 hullForce;
    private Vector3 hullRotationForce;

    // Start is called before the first frame update
    void Start()
    {
        myPhysicsObject = GetComponent<PhysicsObject>();

        // Get the initial offset between the camera and the tank
        offsetOfTankAndCamera = Camera.main.transform.position - turret.transform.position;

        // Get the initial rotation difference between the camera and the tank
        rotationOfTankAndCamera = Quaternion.Inverse(turret.transform.rotation) * Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        TankHullMove();
        TankHullRotate();
        TurretRotation();
        //CameraFallowTankMoveAndRotate();
    }

    /// <summary>
    /// move the tank only move fowrd and back 
    /// </summary>
    public void TankHullMove()
    {
        // 获取坦克当前朝向的前方方向
        Vector3 forwardDirection = transform.forward;
        
        // 根据玩家输入和坦克朝向计算力
        Vector3 force = forwardDirection * movementInput.y * hullPower * 100f;
        
        // 限制力的大小
        force = Vector3.ClampMagnitude(force, maxHullForce);
        
        Debug.Log(forwardDirection);
        myPhysicsObject.ApplyForce(force);
        
        if (myPhysicsObject.velocity.magnitude < 0.2 && movementInput.y!=0)
        {
            
            myPhysicsObject.StartMoveing(movementInput.y);
        }

        //transform.Translate(0, 0, movementInput.y * hullPower * Time.deltaTime);
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
        turret.transform.Rotate(Vector3.up, rotateInput.x * turretRotateSpeed * Time.deltaTime );

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

    }
    public void OnMoveForTurretRotation(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>();
        rotateInput.x = ClampValue(rotateInput.x);
        
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
      
        Camera.main.transform.position = turret.transform.position + offsetOfTankAndCamera;
        Camera.main.transform.rotation = turret.transform.rotation * rotationOfTankAndCamera;
    }
}
