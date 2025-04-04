using UnityEngine;

public class PillPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Kolize detekov·na s: " + other.gameObject.name);

        if (other.CompareTag("Player")) // Hr·Ë se dotkne pilulky
        {
            Debug.Log("Hr·Ë narazil na pilulku!");
            Destroy(gameObject); // ZniËÌ pilulku
        }

        /*if (other.CompareTag("Player"))
        {
            if (CompareTag("BluePill"))
            {
                Debug.Log("Modr· pilulka");
            }
            else if (CompareTag("GreenPill"))
            {
                Debug.Log("Zelen· pilulka");
   
            }
            else if (CompareTag("RedPill"))
            {
                Debug.Log("»erven· pilulka");
            }

            Destroy(gameObject);
        }*/
    }
}
