using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    public Light2D myLight;
    public Light2D myLight2;
    public Light2D globalLight;
    public float maxWait = 1f;
    public float maxFlicker = 0.2f;

    private float timer;
    private float interval;
    private bool isFlickering; // New flag to control flickering

    void Update()
    {
        if (isFlickering) // Only toggle light if flickering is enabled
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                ToggleLight();
            }
        }
    }

    void ToggleLight()
    {
        // Toggle myLight
        myLight.enabled = !myLight.enabled;
        interval = myLight.enabled ? Random.Range(0, maxWait) : Random.Range(0, maxFlicker);
        
        // Toggle myLight2
        myLight2.enabled = !myLight2.enabled;
        interval = myLight2.enabled ? Random.Range(0, maxWait) : Random.Range(0, maxFlicker);

        // Toggle globalLight
        globalLight.enabled = !globalLight.enabled;
        interval = globalLight.enabled ? Random.Range(0, maxWait) : Random.Range(0, maxFlicker);

        timer = 0;
    }

    public void CloseLights()
    {
        myLight.enabled = false; 
        myLight2.enabled = false;
        globalLight.enabled = false;

    }

    public void EventLight()
    {
        isFlickering = true; // Start flickering
        timer = 0; // Reset timer
    }

    // Method to stop flickering and reset lights to a normal state
    public void StopEventLight()
    {
        isFlickering = false; // Stop flickering
        ResetLights(); // Reset lights to a default state
    }

    void ResetLights()
    {
        myLight.enabled = true; 
        myLight2.enabled = true;
        globalLight.enabled = true;
    }
}
