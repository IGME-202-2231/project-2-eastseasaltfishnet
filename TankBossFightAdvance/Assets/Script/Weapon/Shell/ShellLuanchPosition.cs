using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellLuanchPosition : MonoBehaviour
{
    public GameObject shellPrefab; 
    public Transform launchPosition; 
    public float reloadTime = 5f; 

    private float timer;
    public GameObject explosionPrefab;

    private void Start()
    {
        timer = reloadTime; 
    }

    private void Update()
    {
        //fire when finish realod 
        timer += Time.deltaTime;

        
        if (timer >= reloadTime)
        {
            FireShell();
            Explode();
            timer = 0f; 
        }
    }
    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, launchPosition.position, Quaternion.identity);
        Destroy(explosion, 2.0f);
    }
    private void FireShell()
    {
        GameObject shellInstance = Instantiate(shellPrefab, launchPosition.position, launchPosition.rotation);
    }
}
