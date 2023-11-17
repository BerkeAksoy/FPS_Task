using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{

    private Transform cam;

    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
