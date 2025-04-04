using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject pillPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(1, 24), (float)0.3, Random.Range(1, 24));
            Instantiate(pillPrefab, randomSpawnPos, Quaternion.identity);
        }
    }
}
