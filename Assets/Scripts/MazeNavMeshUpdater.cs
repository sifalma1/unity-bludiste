using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MazeNavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface surface; // Odkaz na NavMeshSurface

    void Start()
    {
        GenerateMazeAndBakeNavMesh();
    }

    public void GenerateMazeAndBakeNavMesh()
    {
        Invoke(nameof(BakeNavMesh), 5f);
    }

    void BakeNavMesh()
    {
        surface.BuildNavMesh();
    }
}