using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector2 mouseLook, smooth;
    public float sensitivity;
    [SerializeField] float smoothness;
    float oldSensitivity;
    GameObject character;
    public MovementScript player;

    public Transform playerBody;

    float timer = 0;
    float timeInterval = 2;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.parent.gameObject;

        oldSensitivity = sensitivity;

    }


    void LateUpdate()
    {
        if(!MovementScript.isDead)
        {
            var mousedir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            mousedir = Vector2.Scale(mousedir, new Vector2(sensitivity * smoothness * Time.deltaTime, sensitivity * smoothness * Time.deltaTime));
            smooth.x = Mathf.Lerp(smooth.x, mousedir.x, 1f / smoothness);
            smooth.y = Mathf.Lerp(smooth.y, mousedir.y, 1f / smoothness);

            mouseLook += smooth;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -60f, 60f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }

        Death();
    }

    void Death()
    {
        if (MovementScript.isDead)
        {
            timer += Time.deltaTime;
            if (timer <= timeInterval)
                transform.Rotate(Vector3.right, -30 * Time.deltaTime);
        }
        else
            timer = 0;
    }
}
