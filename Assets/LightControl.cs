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
    private bool isFlickering;

    void Update()
    {
        if (isFlickering) //if flickering is enabled
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                ToggleLight(); //call toggle light function
            }
        }
    }

    void ToggleLight()
    {
        //toggle light over door
        myLight.enabled = !myLight.enabled;

        //flicker at random intervals and random intensity
        interval = myLight.enabled ? Random.Range(0, maxWait) : Random.Range(0, maxFlicker);
        
        //toggle light over door
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
        isFlickering = true; //start flickering
        timer = 0; //reset timer
    }

    public void StopEventLight()
    {
        isFlickering = false; //stop flickering
        ResetLights(); //reset lights
    }

    void ResetLights()
    {
        myLight.enabled = true; 
        myLight2.enabled = true;
        globalLight.enabled = true;
    }
}
