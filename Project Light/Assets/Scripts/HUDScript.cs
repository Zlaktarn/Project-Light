using System.Collections;
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
        oxygenSlider.value = playerScript.Oxygen;

        //OxygenColor();
    }

    void OxygenColor()
    {
        float rest = 100 - playerScript.Oxygen;
        green = playerScript.Oxygen * 2.55f;
        red = rest * 2.55f;



        fillImage.color = new Color32((byte)red, 0, (byte)green, 255);
    }
}
