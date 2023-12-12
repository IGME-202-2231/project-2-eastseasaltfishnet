using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretAimAndFire : MonoBehaviour
{
    public GameObject turret;
    public float rotateSpeed;

    private Transform target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
    }
    void Update()
    {
        EnemyAimPlayer(turret,target.position, rotateSpeed);
    }

    private void EnemyAimPlayer(GameObject turret, Vector3 targetPos, float rotateSpeed )
    {
        Vector3 flatTargetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        Vector3 targetDirection = (flatTargetPos - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion slerpedRotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        turret.transform.rotation = Quaternion.Euler(0, slerpedRotation.eulerAngles.y, 0);
    }
}
