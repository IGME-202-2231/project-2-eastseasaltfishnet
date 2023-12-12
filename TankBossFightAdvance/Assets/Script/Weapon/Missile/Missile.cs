using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Missile : Agent
{
    public float avoidWallRayDetectionDistance;
    public float turningSpeed;

    public float advoidDistance;
    public float advoidForce;

    // Start is called before the first frame update
    private Transform target;
    private Vector3 SeekingForce;

    private float avoidWallTimer = 0f;
    private float avoidWallDuration = 0.05f;

    // if will let the missile be able to get away from the wall when it was launched
    private float lookPlayertimer=5f;
    private float avoidPlayerDuration = 5f;

    private Vector3 upwardDirectionOne;
    private Vector3 downwardDirectionOne;
    private Vector3 downwardDirectionTwo;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
        myPhysicsObject = GetComponent<PhysicsObject>();
    }
    /// <summary>
    /// It contain the detction of wall here, I tried to put this into another method, but it doesn't work well. The missiles twitch a lot
    /// </summary>
    protected override void CalcSteeringForces()
    {
        upwardDirectionOne = Quaternion.Euler(5, 0, 0) * transform.forward;
        downwardDirectionOne = Quaternion.Euler(-5, 0, 0) * transform.forward;
        downwardDirectionTwo = Quaternion.Euler(-10, 0, 0) * transform.forward;

        //this timer is for when advoiding the wall prevent the missile from instanly look for player when adoving the wall and cause twitch;
        avoidWallTimer += Time.deltaTime;

        //this timer is for when the player infront of the wall, stop advoding wall during this situation
        lookPlayertimer += Time.deltaTime;


        //Prevents missiles from jerking while avoiding walls
        //(thinking they've avoided the wall too quickly and then righting themselves)
        //so sent out 3 rays 
        Vector3[] directionsWall = { transform.forward, downwardDirectionOne, downwardDirectionTwo };

        //the angle between detecting player will need to be smaller, because the target is much smaller
        Vector3[] directionsPlayer = { upwardDirectionOne/3f,transform.forward, downwardDirectionOne/3f, downwardDirectionTwo/3f };
        bool wallDetected = false;
        bool noPlayer = true;


        //detect for the player first 
        foreach (Vector3 dir in directionsPlayer)
        {
            //if the tank is infront of the wall and the ray to detected the player will need to be longer to prevent problem.
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, avoidWallRayDetectionDistance + 10f) && hit.collider.CompareTag("PlayerTank"))
            {

                SeekingForce = MissileSeek(target.position, turningSpeed);
                noPlayer = false;
                
                


                //reset the timer to make sure the missile is looking for player no advoiding the wall
                lookPlayertimer = 0f;
                avoidWallTimer = 0f;
                break;

            }
        }

        //if there is no player at the front 
        if (lookPlayertimer> avoidPlayerDuration)
        {
            if (noPlayer)
            {
                foreach (Vector3 dir in directionsWall)
                {
                    if (Physics.Raycast(transform.position, dir, out RaycastHit hit, avoidWallRayDetectionDistance) && hit.collider.CompareTag("Wall"))
                    {
                        SeekingForce = MissileAvoidWall(turningSpeed);
                        wallDetected = true;
                        //reset the timer so the missile wont seek target 
                        avoidWallTimer = 0;
                        break;
                    }
                }
            }
        }

        if (avoidWallTimer > avoidWallDuration)
        {
            if (!wallDetected)
            {
                SeekingForce = MissileSeek(target.position, turningSpeed);
            }
        }

        SeekingForce += MissileSeparation(advoidDistance,advoidForce);
        
        myPhysicsObject.ApplyForce(SeekingForce);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward*100f);

        // draw all the ray
        Vector3 downwardDirectionOne = Quaternion.Euler(5, 0, 0) * transform.forward;
        Vector3 downwardDirectionTwo = Quaternion.Euler(10, 0, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionOne * 100f);
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionTwo * 100f);

        Gizmos.color = Color.yellow;

       Vector3  DirectionOne = Quaternion.Euler(-5 / 3, 0, 0) * transform.forward;
        downwardDirectionOne = Quaternion.Euler(5/3, 0, 0) * transform.forward;
         downwardDirectionTwo = Quaternion.Euler(10/3, 0, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + DirectionOne * 110f);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 110f);
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionOne * 110f);
        Gizmos.DrawLine(transform.position, transform.position + downwardDirectionTwo * 110f);
    }
}
