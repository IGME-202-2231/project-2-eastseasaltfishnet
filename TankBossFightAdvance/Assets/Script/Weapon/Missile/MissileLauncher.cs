using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MissileLauncher : MonoBehaviour
{

    public GameObject Missile;

    public List<GameObject> launchPositionSet;
    public float currentMissileNumber = 2f;

    public float reloadAllMissileTime;
    


    private int countForLaunchPosition;

    private float timer;
    private float shortTimer;
    private float remainMissileNumber;
    private float timeBetweenEachLaunch;

    private void Start()
    {
        countForLaunchPosition = 0;
        remainMissileNumber = currentMissileNumber;
        timeBetweenEachLaunch = 0.2f;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        shortTimer+= Time.deltaTime;
        // launch a missile when the timere reach the time
        if (timer > reloadAllMissileTime)
        {
            //wait for the time between each burst
            if (shortTimer > timeBetweenEachLaunch)
            {
                Vector3 launchPosition = launchPositionSet[countForLaunchPosition].transform.position + transform.forward;

                Instantiate(Missile, launchPosition, transform.rotation);

                remainMissileNumber--;
                shortTimer = 0;
                countForLaunchPosition++;

                if (countForLaunchPosition >= launchPositionSet.Count)
                {
                    countForLaunchPosition = 0;
                }
            }
            
        }

        //reset the timer when no missile
        if(remainMissileNumber<=0)
        {
            remainMissileNumber = currentMissileNumber;
            timer = 0;
        }
    }
}
