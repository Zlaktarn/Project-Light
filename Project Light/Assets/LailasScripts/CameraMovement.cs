using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector2 mouseLook, smooth;
    public float sensitivity = 5.0f;
    public float smoothness = 2.0f;

    GameObject character;

    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var mousedir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mousedir = Vector2.Scale(mousedir, new Vector2(sensitivity * smoothness, sensitivity * smoothness));
        smooth.x = Mathf.Lerp(smooth.x, mousedir.x, 1f / smoothness);
        smooth.y = Mathf.Lerp(smooth.y, mousedir.y, 1f / smoothness);

        mouseLook += smooth;
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
