using UnityEngine;

public class skyboxRotation : MonoBehaviour
{
    public Material skybox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.4f);
    }
}
