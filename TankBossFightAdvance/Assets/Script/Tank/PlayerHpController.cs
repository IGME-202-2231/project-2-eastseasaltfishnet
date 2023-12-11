using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : MonoBehaviour
{
    public int playerHealth = 100;

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

    }
}
