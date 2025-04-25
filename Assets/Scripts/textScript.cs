using UnityEngine;
using TMPro;


public class textScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bluePill;
    [SerializeField] TextMeshProUGUI redPill;
    [SerializeField] TextMeshProUGUI greenPill;

    void Start()
    {
        
    }

    void Update()
    {
        int greenPillCount = GameObject.Find("Player").GetComponent<PlayerMovement>().greenPillCount;
        int bluePillCount = GameObject.Find("Player").GetComponent<PlayerMovement>().bluePillCount;
        int redPillCount = GameObject.Find("Player").GetComponent<PlayerMovement>().redPillCount;

        greenPill.text = "Teleport: " + greenPillCount;
        redPill.text = "Neviditelnost: " + redPillCount;
        bluePill.text = "Skok: " + bluePillCount;
    }
}
