using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject doorPrefab;

    public void SpawnDoors()
    {
        Vector3 pos = new Vector3(12, 0.3f, 12);
        Instantiate(doorPrefab, pos, Quaternion.identity);
        print("Doors spawned!");
    }
}
