using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AgentManger : MonoBehaviour
{
    public static AgentManger Instance;
    public GameObject enemy;
    public Vector3 enemySpawnPosition;

    public GameObject hpBoxPrefab; 
    public Vector3 HpBoxSpawnPosition;
    public float spawnInterval; 

    public List<GameObject> allMissiles = new List<GameObject>();
    public List<GameObject> allEnemy = new List<GameObject>();
    public List<GameObject> allHpBox = new List<GameObject>();

    private float timer;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnOneEnemy();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer> spawnInterval)
        {
            //only spaw it when there is no hp box
            if (allHpBox.Count == 0)
            {
               
                SpawnHpBox(hpBoxPrefab);
                SpawnHpBox(hpBoxPrefab);
                SpawnHpBox(hpBoxPrefab);
                timer = 0;
            }
            
        }
    }

    public void SpawnOneEnemy()
    {
        GameObject enemyAgentManger = Instantiate(enemy, enemySpawnPosition, Quaternion.Euler(0, 90, 0));
        allEnemy.Add(enemyAgentManger);
    }

    public void SpawnMissile(GameObject missilePrefab, Vector3 position, Quaternion rotation)
    {
        GameObject missile = Instantiate(missilePrefab, position, rotation);
        allMissiles.Add(missile);
    }

    public void SpawnHpBox(GameObject HpBox )
    {
        GameObject hpBoxs = Instantiate(HpBox, HpBoxSpawnPosition, Quaternion.Euler(0, 90, 0));
        allHpBox.Add(hpBoxs);
    }
}
