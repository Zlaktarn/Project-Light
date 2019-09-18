using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController charController;

    public int health = 100;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Controls();

        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void Controls()
    {
        float hInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vInput;
        Vector3 rightMovement = transform.right * hInput;

        charController.SimpleMove(forwardMovement + rightMovement);
    }
}
