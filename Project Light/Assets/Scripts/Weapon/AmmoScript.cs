using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    public int totalGunAmmo;
    public int totalCrossbowAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (totalGunAmmo < 0)
            totalGunAmmo = 0;

        if(totalCrossbowAmmo < 0)
            totalCrossbowAmmo = 0;
    }

    void Gun()
    {

    }
}
