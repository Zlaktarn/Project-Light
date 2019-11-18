﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float oldHealth;
    public float health = 100;
    public float Oxygen = 100;
    public float soundRange = 15;

    private Rigidbody rb;

    #region General Variables
    CharacterController charController;
    float movementSpeed;
    float oldMovementSpeed;
    float originalHeight; //For camera
    bool isJumping;
    bool isDodging;
    public bool isDead;
    #endregion

    #region Movement Variables
    [SerializeField] float walkSpeed = 4;
    [SerializeField] float runSpeed = 8;
    [SerializeField] float runBuildUpSpeed = 2;
    [SerializeField] float crouchSpeed = 2;
    [SerializeField] float crouchHeight = 0.9f;
    Vector3 rightMovement;
    #endregion

    #region Slope Variables
    [SerializeField] float slopeForce = 6;
    [SerializeField] float slopeForceRayLength = 1.5f;
    #endregion

    #region Jump Variables
    [SerializeField] AnimationCurve jumpFallOff;
    [SerializeField] float jumpMultiplier = 7;
    [SerializeField] float dodgeMultiplier = 1.5f;
    [SerializeField] float dodgeSpeed = 12f;
    #endregion

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        originalHeight = charController.height;
        oldMovementSpeed = movementSpeed;
    }

    void Update()
    {
        PlayerControls();

        if (Input.GetKeyDown(KeyCode.N))
            SavePlayer();
        if (Input.GetKeyDown(KeyCode.M))
            LoadPlayer();


            
    }

    #region Movement
    void PlayerControls()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * vInput;
        rightMovement = transform.right * hInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        if ((vInput != 0 || hInput != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);

        if (vInput == 0 && hInput != 0)
            DodgeInput();
        else
            JumpInput();

        SetMovementSpeed();
        Crouching();
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
            soundRange = 20f;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, crouchSpeed, Time.deltaTime * runBuildUpSpeed);
            soundRange = 10f;
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
            soundRange = 15f;
        }
    }

    #region Slope Method
    private bool OnSlope()
    {
        if (isJumping || isDodging)
            return false;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            return true;
            
        return false;
    }
    #endregion

    #region Jump Method
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float airTime = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(airTime);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);


        charController.slopeLimit = 45.0f;
        isJumping = false;
    }
    #endregion

    #region Dodge Method
    private void DodgeInput()
    {
        float hInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isDodging = true;
            StartCoroutine(DodgeEvent());
        }
    }

    private IEnumerator DodgeEvent()
    {
        float airTime = 0.0f;
        float hInput = Input.GetAxis("Horizontal");

        do
        {
            float jumpForce = jumpFallOff.Evaluate(airTime);
            charController.Move(Vector3.up * jumpForce * dodgeMultiplier * Time.deltaTime);
            charController.Move(Camera.main.transform.right * jumpForce * dodgeSpeed * hInput * Time.deltaTime);
            airTime += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        isDodging = false;
    }
    #endregion

    #region Crouch Method
    void Crouching()
    {
         //Vector3 centerPoint = charController.center = new Vector3(0, charController.height / 2, 0);

        if (Input.GetKey(KeyCode.LeftControl))  //SKAPAR EN BUG FÖR ONSLOPE()
            charController.height = crouchHeight;
        else
            charController.height = originalHeight;
    }
    #endregion
    #endregion

    #region Save&Load
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    #endregion
}
