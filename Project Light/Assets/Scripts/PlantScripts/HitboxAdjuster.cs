using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxAdjuster : MonoBehaviour
{
    // Hitbox Variables
    private Vector3 tempScale;
    private float scale;
    public float scaleOffset = 2;
    
    // Common Variables
    private Light lt;
    private GameObject parentObject;

    void Start()
    {
        parentObject = transform.parent.gameObject;
        lt = parentObject.GetComponentInChildren<Light>();
    }

    void Update()
    {
        scale = lt.range;
        scale *= scaleOffset;
        tempScale = new Vector3(scale, scale, scale);
        transform.localScale = tempScale;
    }
}
