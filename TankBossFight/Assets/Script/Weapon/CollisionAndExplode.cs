using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionAndExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosionPrefab;

    public float selfDestructTimer = 6.0f; // 导弹自毁的时间
    private bool hasExploded = false;
    // Update is called once per frame
    void Update()
    {
        //find is there any object collding with the missile
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        if (hitColliders.Length >1)
        {
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
        //call the explsoion
        Destroy(explosion, 2.0f);
        Destroy(gameObject);
    }
}
