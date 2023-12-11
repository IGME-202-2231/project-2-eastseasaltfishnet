using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamagePart : MonoBehaviour
{
    public PlayerHpController playerHpController;

    void Start()
    {
        playerHpController = GetComponentInParent<PlayerHpController>();
    }

    public void ReceiveDamage(int damage)
    {

        playerHpController.TakeDamage(damage);


    }
}
