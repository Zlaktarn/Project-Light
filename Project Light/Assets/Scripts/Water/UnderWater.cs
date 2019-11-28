using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public float waterLevel = 22.5f;
    private bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;

    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.02f, 0.45f, 0.57f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterLevel;
            if(isUnderwater) SetUnderWater();
            if(!isUnderwater) SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.5f;
    }

    void SetUnderWater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.03f;
    }
}
