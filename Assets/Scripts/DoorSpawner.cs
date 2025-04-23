using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject doorPrefab;
    public int mazeSize;

    public void SpawnDoors()
    {
        Vector3 pos = new Vector3(mazeSize/2, 0.3f, mazeSize/2);
        Instantiate(doorPrefab, pos, Quaternion.identity);
        print("Doors spawned!");
    }
}
