using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    bool Triggered = false;
    bool pickedUp = false;
    private Color oldColor = Color.red;
    Renderer render;

    [SerializeField] public int itemID;

    private void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = oldColor;
    }

    void Update()
    {
        if(Triggered)
        {
            if (Input.GetKey(KeyCode.E))
            {
                render.material.color = Color.green;
                //Destroy(gameObject);
            }
            else
                render.material.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        oldColor = render.material.color;
        render.material.color = Color.yellow;

        Triggered = true;
        print(Triggered);
    }

    private void OnTriggerExit(Collider other)
    {
        render.material.color = oldColor;

        Triggered = false;
        print(Triggered);
    }
}
