using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 randomSpawnPos = new Vector3(0, (float)0.3, 0);
        Instantiate(keyPrefab, randomSpawnPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
