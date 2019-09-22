using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    Animator anim;
    Camera cam;
    Interactable InterA;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift))
            anim.SetInteger("Condition", 1);

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetInteger("Condition", 2);
            
        }
           
        else
            anim.SetInteger("Condition", 0);

        
    }
}
