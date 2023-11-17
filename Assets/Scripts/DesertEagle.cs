using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEagle : Weapon
{
    void Start()
    {
        fireSpeed = 1f; // Saniyede 1 dakikada 60 kez
        damage = 40f;
        capacity = 10;
        reloadLength = 4f;
        accuracy = 90;
        curAmmo = capacity;
    }
}
