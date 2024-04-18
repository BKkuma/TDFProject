using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnamySpawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject[] enamyPhefab;

    [Header("Attributes")]
    [SerializeField] private int baseEnamy = 8;
    [SerializeField] private float enamyPersecond = 0.5f;
    [SerializeField] private float timeBetweenWave = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnamyDestroy = new UnityEvent();


     private int currentWave = 1;
     private float timeSinceLastSpawn;
     private int enamyAlive;
     private  int enamyLeftToSpawn;
     private bool isSpawning = false;
    private void Awake()
    {
        onEnamyDestroy.AddListener(EnamyDestroyed);
    }
    private void Start()
    {
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isSpawning) 
        { 
            return; 
        }
        
            timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn >= ( 1f / enamyPersecond ) && enamyLeftToSpawn > 0)
        {
            SpawnEnamy();
            enamyLeftToSpawn--;
            enamyAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enamyAlive == 0 && enamyLeftToSpawn == 0) 
        {
            EndWave();
        }
    }

    private void SpawnEnamy() 
    {
        GameObject phefabToSpawn = enamyPhefab[0];
        Instantiate(phefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    private void EnamyDestroyed()
    {
        enamyAlive--;
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());

    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWave);
        isSpawning = true;
        enamyLeftToSpawn = EnamyPerWave();

    }

    private int EnamyPerWave()
    {
        return Mathf.RoundToInt(baseEnamy * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
