using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    public int gAmmoTotal;
    public int rAmmoTotal;
    public int cbAmmoTotal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gAmmoTotal < 0)
            gAmmoTotal = 0;

        if (rAmmoTotal < 0)
            rAmmoTotal = 0;

        if (cbAmmoTotal < 0)
            cbAmmoTotal = 0;
    }

    void Gun()
    {

    }
}
