using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public static int selectedWeapon;
    private Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        selectWeapon();
    }

    void Update()
    {
        int pSW = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }

        if(pSW != selectedWeapon)
        {
            selectWeapon();
        }
    }

    public void selectWeapon()
    {
        int i = 0;

        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                Weapon w = weapon.GetComponent<Weapon>();
                UIManager.Instance.updateAmmoText(w);
                player.setWInUse(w);
                Debug.Log("Silah Değiştirildi. Yeni Silah " + w.name);
            }
            else
            {
                Weapon w = weapon.GetComponent<Weapon>();
                w.StopAllCoroutines();
                Destroy(GameObject.Find("One shot audio"));
                UIManager.Instance.setReloadTextA(0);
                w.setInreload(false);
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }
}
