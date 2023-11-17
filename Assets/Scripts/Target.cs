using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamagable
{
    private const float maxHealth = 100;
    private bool isAlive = true;
    private float curHealth = 0;
    private Vector3 startPos;
    private Animator animator;
    private HealthBar healthBar;
    [SerializeField]
    private AudioClip ouch, death;

    private void Start()
    {
        startPos = transform.position;
        animator = GetComponentInChildren<Animator>();
        healthBar = GameObject.Find(name + "/Canvas/Health Bar").GetComponent<HealthBar>();
        curHealth = maxHealth;
        healthBar.updateHealth(this);
    }

    public void takeDamage(float value)
    {
        if (isAlive)
        {
            if (curHealth > 0)
            {
                Debug.Log("Hedefe isabet etti");
                AudioSource.PlayClipAtPoint(ouch, transform.position);
                curHealth -= value;
                healthBar.updateHealth(this);
                animator.SetTrigger("isHit");
            }

            if (curHealth <= 0)
            {
                die();
            }
        }
    }

    private void die()
    {
        AudioSource.PlayClipAtPoint(death, transform.position);
        isAlive = false;
        Debug.Log("Hedef Kullanılamaz halde");
        animator.SetTrigger("Die");
    }

    public void refreshHeath()
    {
        Debug.Log("Targets Healed!");
        transform.position = startPos;
        isAlive = true;
        animator.SetTrigger("Stand");
        curHealth = maxHealth;
        healthBar.updateHealth(this);
    }

    public float getCurHealth()
    {
        return curHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
