using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject doorPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 randomSpawnPos = new Vector3(24, (float)0.3, 24);
        Instantiate(doorPrefab, randomSpawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
