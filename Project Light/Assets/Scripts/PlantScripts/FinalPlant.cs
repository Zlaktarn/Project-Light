using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlant : MonoBehaviour
{
    public EnemyManager em;
    public GameObject mainPlant;
    public GameObject hand;

    void Start()
    {
        if(em.IsBossDead)
            if(IsMainPlantHeld()){}
                // win the game;
    }

    void Update()
    {
        
    }

    bool IsMainPlantHeld()
    {
        if(mainPlant.transform.parent == hand)
            return true;
        return false;
    }
}
