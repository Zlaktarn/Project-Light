using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    float timer = 0.0f;
    [SerializeField] float bobbingSpeed = 0.12f;
    float oldBobbingSpeed;
    [SerializeField] float bobbingAmount = 0.09f;
    [SerializeField] float midPoint = 1.6f;

    private void Start()
    {
        oldBobbingSpeed = bobbingSpeed;
    }

    void Update()
    {
        if(!MovementScript.isDead)
        {
            float waveSlice = 0.0f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
                timer = 0.0f;
            else
            {
                waveSlice = Mathf.Sin(timer);
                timer = timer + bobbingSpeed;
                if (timer > Mathf.PI * 2)
                    timer = timer - (Mathf.PI * 2);
            }

            Vector3 v3T = transform.localPosition;
            if (waveSlice != 0)
            {
                float translateChange = waveSlice * bobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
                translateChange = totalAxes * translateChange;
                v3T.y = midPoint + translateChange;
            }
            else
                v3T.y = midPoint;

            transform.localPosition = v3T;

            if (PauseMenu.GameIsPaused)
                bobbingSpeed = 0;
            else
                bobbingSpeed = oldBobbingSpeed;
        }
    }
}
