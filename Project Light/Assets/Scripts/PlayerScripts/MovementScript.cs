using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] string horizontalInputName;
    [SerializeField] string verticalInputName;
    CharacterController charController;

    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float runBuildUpSpeed;
    [SerializeField] KeyCode runKey;
    float movementSpeed = 4;

    [SerializeField] float slopeForce;
    [SerializeField] float slopeForceRayLength;

    [SerializeField] AnimationCurve jumpFallOff;
    [SerializeField] float jumpMultiplier;
    [SerializeField] KeyCode jumpKey;
    bool isJumping;

    public int health = 100;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerControls();
    }

    void PlayerControls()
    {
        float hInput = Input.GetAxis(horizontalInputName);
        float vInput = Input.GetAxis(verticalInputName);

        Vector3 forwardMovement = transform.forward * vInput;
        Vector3 rightMovement = transform.right * hInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        if ((vInput != 0 || hInput != 0) && OnSlope())
        {
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);
        }

        JumpInput();
        SetMovementSpeed();
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if(hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
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
}
