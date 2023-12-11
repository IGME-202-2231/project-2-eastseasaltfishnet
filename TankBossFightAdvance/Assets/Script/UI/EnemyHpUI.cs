using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image EnemyHpBar;

    private float EnemyMaxHealth;
    private float EnemyCurrentHealth;

    public EnemyHpController EnemyHpController;

    private void Start()
    {
        EnemyHpController = GetComponent<EnemyHpController>();

        EnemyMaxHealth = EnemyHpController.enemyHealth;

    }
    private void Update()
    {
        EnemyHpController = GetComponent<EnemyHpController>();
        EnemyCurrentHealth = EnemyHpController.enemyHealth;

        EnemyHpBar.fillAmount = EnemyCurrentHealth / EnemyMaxHealth;
    }
}
