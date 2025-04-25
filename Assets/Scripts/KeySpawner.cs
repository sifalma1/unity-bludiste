using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public int mazeSize;
    float y = 0.3f;

  
    
    public GameObject keyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int edge = Random.Range(0, 4);
        int pos = Random.Range(1, mazeSize - 1);
        Vector3 spawnPos = Vector3.zero;

        switch (edge)
        {
            case 0: 
                spawnPos = new Vector3(0, y, pos);
                break;
            case 1: 
                spawnPos = new Vector3(mazeSize, y, pos);
                break;
            case 2: 
                spawnPos = new Vector3(pos, y, 0);
                break;
            case 3: 
                spawnPos = new Vector3(pos, y, mazeSize);
                break;
        }
        Instantiate(keyPrefab, spawnPos, Quaternion.identity);
    }
}
