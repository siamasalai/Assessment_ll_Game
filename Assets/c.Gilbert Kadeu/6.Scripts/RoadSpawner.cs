using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    // Change this to an array to hold multiple city prefabs
    public GameObject[] cityPrefabs;
    public Transform player;

    private float spawnZ = 0;
    private float tileLength;
    private int safeDistance = 6;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        // Calculate length based on the first prefab 
        // (Assumes both cities are the same length)
        if (cityPrefabs.Length > 0)
        {
            tileLength = cityPrefabs[0].GetComponentInChildren<MeshRenderer>().bounds.size.z;
        }

        for (int i = 0; i < safeDistance; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (player.position.z > (spawnZ - (safeDistance * tileLength)))
        {
            SpawnTile();
            DeleteOldTile();
        }
    }

    void SpawnTile()
    {
        // Pick a random city from your list
        int randomIndex = Random.Range(0, cityPrefabs.Length);

        GameObject go = Instantiate(cityPrefabs[randomIndex], new Vector3(0, 0, spawnZ), Quaternion.identity);
        activeTiles.Add(go);
        spawnZ += tileLength;
    }

    void DeleteOldTile()
    {
        if (activeTiles.Count > safeDistance + 2)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}