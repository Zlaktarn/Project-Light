using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAdjuster : MonoBehaviour
{

    // Color Variables
    private Color endColor;
    public Color startColor;
    private float emissionIntensity = 1f;
    public bool colorEnabled = true;

    // Intensity Variables
    public float startIntensity = 0f;
    public float maxIntensity = 1f;
    public bool intensityEnabled = true;

    // Range Variables
    public float startRange = 0f;
    public float maxRange = 10f;
    public bool rangeEnabled = true;

    // Scale Variables
    public Vector3 startScale = new Vector3(0.15f, 0.15f, 0.15f);
    public Vector3 maxScale = new Vector3(0.8f, 0.8f, 0.8f);
    public bool scaleEnabled = true;

    // Common Variables
    public float duration = 5f;
    private float colorTime, intensityTime, rangeTime, scaleTime;
    public bool isPlanted = false;
    private Light lt;
    private Material mat;

    // Sets the starting intensity, the end color and gets the light and material components
    void Awake()
    {
        lt = GetComponentInChildren<Light>();
        mat = GetComponent<Renderer>().material;
        lt.intensity = startIntensity;
        colorTime = 0f;
        intensityTime = 0f;
        rangeTime = 0f;
    }

    private void Planted()
    {
        if (isPlanted)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    // Changes the intensity, color and range over time until target is reached
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.C))
            isPlanted = true;

        if (isPlanted)
        {
            if (colorEnabled)
            {
                colorTime += Time.deltaTime/duration;
                mat.SetColor("_EmissionColor", Color.Lerp(startColor, (endColor * emissionIntensity), colorTime));
                if (startColor == endColor)
                    colorEnabled = false;
            }

            if (intensityEnabled)
            {
                intensityTime += Time.deltaTime/duration;
                lt.intensity = Mathf.Lerp(startIntensity, maxIntensity, intensityTime);
                if (lt.intensity >= maxIntensity)
                    intensityEnabled = false;
            }

            if (rangeEnabled)
            {
                rangeTime += Time.deltaTime/duration;
                lt.range = Mathf.Lerp(startRange, maxRange, rangeTime);
                if (lt.range >= maxRange)
                    rangeEnabled = false;
            }

            if (scaleEnabled)
            {
                scaleTime += Time.deltaTime/duration;
                transform.localScale = Vector3.Lerp(startScale, maxScale, scaleTime);
                if(transform.localScale == maxScale)
                    scaleEnabled = false;
            }
        }
    }
}
