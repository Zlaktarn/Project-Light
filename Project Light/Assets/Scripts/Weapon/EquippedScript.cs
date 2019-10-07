using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedScript : MonoBehaviour
{
    private GameObject Crossbow;

    // Start is called before the first frame update
    void Start()
    {
        Crossbow = GameObject.Find("Crossbow");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Crossbow.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            Crossbow.SetActive(true);
    }
}
