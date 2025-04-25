using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
 

public class RandomMovement : MonoBehaviour 
{
    public NavMeshAgent agent;
    public float range;
    public float targetTime = 4.0f;

    public Transform centrePoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.3f)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) 
            {
                agent.SetDestination(point);
            }
            targetTime = 4.0f;
        }
    }



    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
     
        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {

                result = hit.position;
                return true;
        }

        result = Vector3.zero;
        return false;
    }


}