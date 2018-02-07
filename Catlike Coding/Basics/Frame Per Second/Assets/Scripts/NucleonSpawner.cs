using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleonSpawner : MonoBehaviour {

    public float timeBetweenSpawn;
    public float spawnDistance;
    public float timeSinceLastSpawn;

    public Nucleon[] nucleonPrefabs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn > timeBetweenSpawn)
        {
            timeSinceLastSpawn -= timeBetweenSpawn;
            SpawnNucleon();
        }
    }

    private void SpawnNucleon()
    {
        Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
        Nucleon spawn = Instantiate<Nucleon>(prefab);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
