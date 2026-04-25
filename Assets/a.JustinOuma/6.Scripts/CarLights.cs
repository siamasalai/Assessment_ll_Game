using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    public Light[] headlights;
    public Light[] brakeLights;

    void Update()
    {
        // Toggle headlights (press H)
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (Light light in headlights)
            {
                light.enabled = !light.enabled;
            }
        }

        // Brake lights (press S / Down Arrow)
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            foreach (Light light in brakeLights)
            {
                light.intensity = 3;
            }
        }
        else
        {
            foreach (Light light in brakeLights)
            {
                light.intensity = 1;
            }
        }
    }
}