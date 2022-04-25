using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : HealthConroller
{
    public Image imgHealthBar;
    public GameObject Blood;
    public float Health = 3;
    public int MaxHealth = 3;
    public GameObject DeathPanel;
    Animator animator;

    public CharacterState State;

    //set a cooldown for some time, thus damage will apply every x seconds
    bool isInCooldown = false;
    public bool isDamaged = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        OnHealthUpdated();
    }

    public override void OnHealthUpdated()
    {
        imgHealthBar.rectTransform.sizeDelta = new Vector2(125 * Health, 120);
    }

    void ResetCooldown()
    {
        isInCooldown = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isInCooldown)
        {
            Invoke("ResetCooldown", 2);
            HandleCollision(collision.gameObject);
            isInCooldown = true;
        }
    }

    public override void HandleCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("InstantKill"))
        {
            int amount = 3;
            SubtractHealth(amount);
            Instantiate(Blood, transform.position, Quaternion.identity);
        }

        if (otherObject.CompareTag("Health"))
        {
            float amount = 0.5f;
            AddHealth(amount);
            Destroy(otherObject);
        }

        if (gameObject.CompareTag("Player"))
        {
            if (otherObject.CompareTag("mainEnemy"))
            {
                MainEnemyController mainEnemy = otherObject.GetComponent<MainEnemyController>();
                int amount = mainEnemy.Damage;
                SubtractHealth(amount);
                Instantiate(Blood, transform.position, Quaternion.identity);
                isDamaged = true;
            }

            if (otherObject.CompareTag("Kill"))
            {
                int amount = 1;
                SubtractHealth(amount);
                Instantiate(Blood, transform.position, Quaternion.identity);
                isDamaged = true;
            }

            if (otherObject.CompareTag("FireBall"))
            {
                float amount = 0.5f;
                SubtractHealth(amount);
                Instantiate(Blood, transform.position, Quaternion.identity);
                isDamaged = true;
            }            

            Recovery();           
        }                     

        base.HandleCollision(otherObject);
    }

    void Recovery()
    {
        if (isDamaged == true)
        {                     
            gameObject.tag = "Untagged"; 
            Invoke("StopRecovery", 2);
        }
    }

    void StopRecovery()
    {
        gameObject.tag = "Player";
        isDamaged = false;
    }

    public void AddHealth(int amount)
    {
        Health += amount;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        OnHealthUpdated();
    }

    public void AddHealth(float amount)
    {
        Health += amount;

        if (Health > 3)
        {
            Health = MaxHealth;
        }

        OnHealthUpdated();
    }

    public void SubtractHealth(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OnDeath();
        }

        OnHealthUpdated();
    }

    public void SubtractHealth(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OnDeath();
        }

        OnHealthUpdated();
    }

    public void DeathPanelActive()
    {
        DeathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public override void OnDeath()
    {
        Invoke("DeathPanelActive", 1.5f);

        base.OnDeath();
    }
}
