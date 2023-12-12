using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : MonoBehaviour
{
    public int playerHealth ;
    private int maxHp;
    private void Start()
    {
        maxHp = playerHealth;
    }
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

    }
    public void Health(int increaseHp)
    {
        playerHealth += increaseHp;
        if(playerHealth>= maxHp)
        {
            playerHealth=maxHp;
        }
    }
}
