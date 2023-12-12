using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAndExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosionPrefab;

    public float selfDestructTimer = 6.0f; 
    private bool hasExploded = false;
    // Update is called once per frame
    void Update()
    {
        //find is there any object collding with the missile
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        if (hitColliders.Length >1)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.tag == "PlayerTank")
                {
                    PlayerTakeDamagePart playerTakeDamage = hitCollider.GetComponent<PlayerTakeDamagePart>();

                    if (playerTakeDamage != null)
                    {
                        playerTakeDamage.ReceiveDamage(3);
                    }
                    else
                    {
                        Debug.LogError("PlayerTakeDamagePart component not found on the hit object.");
                    }

                }
            }
            
            Explode();

        }
        ExplodedCount();
    }

    private void ExplodedCount()
    {
        selfDestructTimer -= Time.deltaTime;

        if (selfDestructTimer <= 0 && !hasExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        hasExploded = true;
        Destroy(explosion, 2.0f);

        // remove from the list
        if (AgentManger.Instance != null)
        {
            AgentManger.Instance.allMissiles.Remove(this.gameObject);
        }

        Destroy(gameObject);
    }
}
