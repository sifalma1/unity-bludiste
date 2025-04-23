using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public int pillCount;
    public GameObject pillPrefab;
    public int mazeSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < pillCount; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(1, mazeSize), (float)0.3, Random.Range(1, mazeSize));
            Instantiate(pillPrefab, randomSpawnPos, Quaternion.identity);
        }
    }
}
