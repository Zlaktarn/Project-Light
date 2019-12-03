using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public bool HasWon = false;
    public bool TestKillBoss = false;
    private bool lerpColor = true;
    private bool finished = false;
    public EnemyManager em;
    public Image image;
    public GameObject tCamera;
    public GameObject mainCamera;
    public GameObject endCredits;
    public GameObject UI;
    public GameObject miniMap;
    public Light tLight;
    public Color startColor, endColor;
    public float duration;
    private float timer, count;

    void Update()
    {
        if(TestKillBoss)
            em.IsBossDead = true;

        if (HasWon)
        {
            timer += Time.deltaTime/duration;
            if(timer >= 1 && !finished)
            {
                timer = 0;
                lerpColor = false;
                finished = true;
            }

            if (lerpColor)
            {
                image.color = Color.Lerp(startColor, endColor, timer);
            }
            else if(!lerpColor)
            {
                image.color = Color.Lerp(endColor, startColor, timer); 
                tLight.intensity = 2f;
                tLight.color = Color.white;
                tCamera.SetActive(true);
                mainCamera.SetActive(false);
                UI.SetActive(false);
                miniMap.SetActive(false);
                endCredits.SetActive(true);
            }
        }
    }
}
