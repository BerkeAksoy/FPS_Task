using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text ammoText, reloadText;

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI Manager is null.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            ammoText = GameObject.Find("Ammo Text").GetComponent<Text>();
            reloadText = GameObject.Find("Reload Text").GetComponent<Text>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        setReloadTextA(0);
    }

    public void setReloadTextA(int value)
    {
        reloadText.color = new Color(reloadText.color.r, reloadText.color.g, reloadText.color.b, value);
    }

    public void updateAmmoText(Weapon weapon)
    {
        ammoText.text = weapon.gameObject.name + "\n Ammo: " + weapon.getCurAmmo().ToString() + " / " + weapon.getCapacity().ToString();
    }

    public IEnumerator Blink(int value)
    {
        int i = 0;
        while (i < value)
        {
            setReloadTextA(1);
            yield return new WaitForSeconds(0.5f);
            setReloadTextA(0);
            yield return new WaitForSeconds(0.5f);
            i++;
        }
    }



}
