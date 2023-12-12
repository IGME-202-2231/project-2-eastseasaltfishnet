using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoxCollision : MonoBehaviour
{
    public string targetTag;



    void Update()
    {
        
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        if (hitColliders.Length > 1)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == targetTag)
                {
                    PlayerTakeDamagePart playerTakeDamage = hitCollider.GetComponent<PlayerTakeDamagePart>();

                    if (playerTakeDamage != null)
                    {
                        playerTakeDamage.Health(5);
                        CleanUp();
                        break; 
                    }
                }
            }
        }
    }

    private void CleanUp()
    {
        if (AgentManger.Instance != null)
        {
            AgentManger.Instance.allHpBox.Remove(gameObject);
        }
        Destroy(gameObject);
    }
}

