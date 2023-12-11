using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpController : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyHealth;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

    }
}
