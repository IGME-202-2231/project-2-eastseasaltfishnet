using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPart : MonoBehaviour
{
    public EnemyHpController enemyHpController;

    void Start()
    {
        enemyHpController = GetComponentInParent<EnemyHpController>();
    }

    public void ReceiveDamage(int damage)
    {

        enemyHpController.TakeDamage(damage);


    }
}
