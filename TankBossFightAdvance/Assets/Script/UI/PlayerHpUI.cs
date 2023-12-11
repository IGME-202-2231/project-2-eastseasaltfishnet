using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image PlayerHpBar;

    private float PLayerMaxHealth;
    private float PLayerCurrentHealth;

    public PlayerHpController playerHpController;

    private void Start()
    {
        playerHpController = GetComponent<PlayerHpController>();

        PLayerMaxHealth = playerHpController.playerHealth;

    }
    private void Update()
    {
        playerHpController = GetComponent<PlayerHpController>();
        PLayerCurrentHealth = playerHpController.playerHealth;

        PlayerHpBar.fillAmount = PLayerCurrentHealth / PLayerMaxHealth;
    }

}
