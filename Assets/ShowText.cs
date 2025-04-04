using UnityEngine;
using UnityEngine.UI;
public class ShowText : MonoBehaviour
{
    public string textValue;
    public Text textElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textElement.text = textValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
