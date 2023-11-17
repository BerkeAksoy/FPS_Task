using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK : Weapon
{
    void Start()
    {
        fireSpeed = 0.15f; // dakikada 400 kez
        damage = 5f;
        capacity = 30;
        reloadLength = 5f;
        accuracy = 60;
        curAmmo = capacity;
    }
}
