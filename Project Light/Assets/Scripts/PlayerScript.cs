using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;


    int forwardSpeed;
    int sideSpeed;

    private CharacterController charController;

    Rigidbody player;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);

        Controls();
    }

    void Controls()
    {
        float hInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vInput;
        Vector3 rightMovement = transform.right * hInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        //if (Input.GetKey(KeyCode.W))
        //    forwardSpeed = 3;
        //else if (Input.GetKey(KeyCode.S))
        //    forwardSpeed = -2;
        //else
        //    forwardSpeed = 0;

        //if (Input.GetKey(KeyCode.A))
        //    sideSpeed = -2;
        //else if (Input.GetKey(KeyCode.D))
        //    sideSpeed = 2;
        //else
        //    sideSpeed = 0;
    }

    
}
