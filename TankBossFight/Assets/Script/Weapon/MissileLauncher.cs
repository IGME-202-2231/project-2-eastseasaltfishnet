using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{

    public GameObject Missile;

    public float timeBetweenEachLaunch;
    private float timer;
    private float launchDistanceFromLauncher;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // launch a missile when the timere reach the time
        if (timer > timeBetweenEachLaunch)
        {
            Vector3 launchPosition = transform.position + transform.forward * launchDistanceFromLauncher;

            Instantiate(Missile, launchPosition, transform.rotation);



            timer = 0f;
        }
    }
}
