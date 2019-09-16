using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    bool Triggered = false;
    private Color oldColor = Color.white;

    void Update()
    {
        if(Triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Renderer render = GetComponent<Renderer>();

        oldColor = render.material.color;
        render.material.color = Color.green;

        Triggered = true;

        print(Triggered);
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer render = GetComponent<Renderer>();
        render.material.color = oldColor;

        Triggered = false;
        print(Triggered);
    }
}
