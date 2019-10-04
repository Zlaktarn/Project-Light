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
    MovementScript player;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        character = this.transform.parent.gameObject;

        oldSensitivity = sensitivity;

    }


    void Update()
    {
        var mousedir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mousedir = Vector2.Scale(mousedir, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
        smooth.x = Mathf.Lerp(smooth.x, mousedir.x, 1f / smoothness);
        smooth.y = Mathf.Lerp(smooth.y, mousedir.y, 1f / smoothness);

        mouseLook += smooth;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -60f, 60f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);


        //if (player.isDead || PauseMenu.GameIsPaused)
        //    sensitivity = 0;
        //else
        //    sensitivity = oldSensitivity;

        if (PauseMenu.GameIsPaused)
        {
            //sensitivity = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!PauseMenu.GameIsPaused)
        {
            //sensitivity = oldSensitivity;
            Cursor.lockState = CursorLockMode.Locked;
        }

        


        Death();
    }

    void Death()
    {
    }
}
