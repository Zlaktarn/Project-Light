using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjuster : MonoBehaviour
{

    // Color Variables
    private Color endColor;
    public Color startColor;
    private float emissionIntensity = 1f;
    public bool colorEnabled;
    public float colorSpeed = 0.5f;

    // Intensity Variables
    public float startIntensity = 0f;
    public float maxIntensity = 1f;
    public bool intensityEnabled;
    public float intensitySpeed = 2f;

    // Range Variables
    public float startRange = 0f;
    public float maxRange = 10f;
    public bool rangeEnabled;
    public float rangeSpeed = 2f;

    // Common Variables
    private Light lt;
    private Material mat;
    private float startTime;

    // Sets the starting intensity, the end color and gets the light and material components
    void Start()
    {
        lt = GetComponentInChildren<Light>();
        mat = GetComponent<Renderer>().material;
        endColor = mat.GetColor("_EmissionColor");
        mat.SetColor("_EmissionColor", startColor);
        lt.intensity = startIntensity;
        startTime = Time.time;
    }

    // Changes the intensity, color and range over time until target is reached
    void Update()
    {
        if (colorEnabled){
            float t = Time.time * colorSpeed;
            mat.SetColor("_EmissionColor", Color.Lerp(startColor, (endColor * emissionIntensity), t));
            if (startColor == endColor){
                colorEnabled = false;
            }
        }

        if (intensityEnabled){
            lt.intensity = Time.time * intensitySpeed;
            if(lt.intensity >= maxIntensity){
                intensityEnabled = false;
            }
        }

        if (rangeEnabled){
            lt.range = Time.time * rangeSpeed;
            if(lt.range >= maxRange){
                rangeEnabled = false;
            }
        }
    }
}
