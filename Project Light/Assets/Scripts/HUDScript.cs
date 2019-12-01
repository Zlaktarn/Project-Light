﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    public Slider healthSlider;
    public Slider oxygenSlider;
    public Image fillImage;
    float green, red;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    private void Update()
    {
        healthSlider.value = playerScript.health;

        OxygenColor();
    }

    void OxygenColor()
    {
        float rest = 100 - playerScript.Oxygen;
        green = playerScript.Oxygen * 2.55f;
        red = rest * 2.55f;

        Debug.Log("Oxygen: " + playerScript.Oxygen + ". RGB = " + red + ", " + green + ", " + "0");

        fillImage.color = new Color32((byte)red, (byte)green, 0, 255);
    }
}
