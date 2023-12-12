using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpController : MonoBehaviour
{
    public int enemyHealth;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

    }
}
