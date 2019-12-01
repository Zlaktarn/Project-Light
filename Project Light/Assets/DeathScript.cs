using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public GameObject deathMenu;

    public Image image;
    Color deathColor;

    private void Start()
    {
    }

    void Update()
    {
        deathColor = image.color;

        if (MovementScript.isDead)
        {
            Death();
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            deathMenu.SetActive(false);
            deathColor.a = 0;
            image.color = deathColor;
        }
    }

    void Death()
    {
        if (deathColor.a <= 1)
        {
            deathColor.a += Time.deltaTime * 0.4f;
            image.color = deathColor;

            if (deathColor.a >= 1)
            {
                Cursor.lockState = CursorLockMode.None;
                deathMenu.SetActive(true);
            }
        }
        
    }

    public void ReturntoMain()
    {
        SceneManager.LoadScene("Menu");
    }
}
