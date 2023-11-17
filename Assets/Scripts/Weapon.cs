using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    protected int capacity, curAmmo, accuracy;
    protected float damage, nextFire = 0, reloadLength, fireSpeed, range = 1000f, force = 4f;
    protected bool inReload;
    [SerializeField]
    protected AudioClip fireSound, emptySound, reloadSound;
    [SerializeField]
    protected ParticleSystem fireEffect;
    protected Animator animator;

    [SerializeField]
    protected Camera fpsCam;

    public void Shoot()
    {
        if (Time.time > nextFire && !inReload)
        {
            if (curAmmo > 0)
            {
                int missChance = Random.Range(0, 100);

                Debug.Log("Ateş Edildi");

                fireEffect.Play();
                AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.2f);
                curAmmo--;
                UIManager.Instance.updateAmmoText(this);

                if (accuracy > missChance)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                    {
                        Debug.Log(hit.transform.name);

                        Target target = hit.transform.GetComponent<Target>();
                        if(target != null)
                        {
                            if(target.GetComponent<IDamagable>() != null) // Means it is damageable
                            {
                                if(hit.rigidbody != null)
                                {
                                    Debug.Log("force added");
                                    hit.rigidbody.AddForce(hit.point * force);
                                }
                                target.takeDamage(damage);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Hedefe isabet etmedi!");
                }

            }
            else
            {
                Debug.Log("Şarjör boş");
                AudioSource.PlayClipAtPoint(emptySound, transform.position, 0.2f);
            }

            nextFire = Time.time + fireSpeed;
        }
    }

    public void reload()
    {
        if(curAmmo < capacity && !inReload)
        {
            inReload = true;
            StartCoroutine(reloadRoutine());
        }
    }

    IEnumerator reloadRoutine()
    {
        AudioSource.PlayClipAtPoint(reloadSound, transform.position);
        StartCoroutine(UIManager.Instance.Blink((int)reloadLength));

        yield return new WaitForSeconds(reloadLength);
        curAmmo = capacity;
        inReload = false;
        Debug.Log("Şarjör dolduruldu.");
        UIManager.Instance.updateAmmoText(this);
    }

    public int getCurAmmo()
    {
        return curAmmo;
    }

    public int getCapacity()
    {
        return capacity;
    }

    public bool isInReload()
    {
        return inReload;
    }

    public void setInreload(bool value)
    {
        inReload = value;
    }


}
