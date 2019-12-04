using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItems : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    public ParticleSystem ps;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(CloseEnough())
                ps.Play();
        else
                ps.Stop(true);
    }

    private bool CloseEnough()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 5f))
        {
            if(Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < 5f)
                return true;
        }

        Vector3 temp = cam.transform.position + cam.transform.forward * 5f;
        if(Vector3.Distance(temp, transform.position) < 5f)
            return true;

        return false;
    }
}
