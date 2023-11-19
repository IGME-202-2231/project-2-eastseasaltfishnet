using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAndExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosionPrefab;
    // Update is called once per frame
    void Update()
    {
        //find is there any object collding with the missile
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        if (hitColliders.Length >1)
        {
            Debug.Log(hitColliders.Length);
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //call the explsoion
            Destroy(explosion, 2.0f);
            Destroy(gameObject);

        }
    }
}
