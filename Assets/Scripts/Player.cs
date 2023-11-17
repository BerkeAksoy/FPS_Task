using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float gravity = 9.81f, speed = 8, yVelocity = 0, sensitivity = 3.5f;
    private CharacterController CharacterController;
    private GameObject fpsCam;

    private Weapon wInUse;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        fpsCam = GameObject.Find("Main Camera");
    }

    void FixedUpdate() // Bu fonksiyonu fizik işlemleri yaptığım fonksiyonlar için kullandım.
    {
        // Taskda yürüme istenmemiş ancak fonksiyon olarak bıraktım. Yorum kaldırılırsa Karakterin W-A-S-D ile yürüdüğü görülecektir.
        // calculateMovement();
        lookX();
        lookY();
    }

    private void Update()
    {
        switch (wInUse.name)
        {
            case "Desert Eagle":
                if (Input.GetKeyDown(KeyCode.S))
                {
                    wInUse.Shoot();
                }
                break;
            case "AK47":
                if (Input.GetKey(KeyCode.S))
                {
                    wInUse.Shoot();
                }
                break;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            wInUse.reload();
        }
    }

    private void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * speed;

        if (CharacterController.isGrounded)
        {
            if (yVelocity != 0)
            {
                yVelocity = 0;
            }
        }
        else
        {
            yVelocity -= gravity;
        }

        velocity.y = yVelocity;
        velocity = transform.transform.TransformDirection(velocity);
        CharacterController.Move(Time.deltaTime * velocity);
    }

    private void lookX()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 newRotation = transform.localEulerAngles;

        newRotation.y += mouseX * sensitivity;

        transform.localEulerAngles = newRotation;
    }

    private void lookY()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 newRotation = fpsCam.transform.localEulerAngles;

        newRotation.x -= mouseY * sensitivity;
        newRotation = new Vector3(newRotation.x, 0, 0);

        fpsCam.transform.localEulerAngles = newRotation;
    }

    public void setWInUse(Weapon weapon)
    {
        wInUse = weapon;
    }
}
