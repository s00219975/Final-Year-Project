using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : HealthConroller
{
    public Image imgHealthBar;
    public GameObject Blood;
    public int Health = 3;
    public int MaxHealth = 3;

    //set a cooldown for some time, thus damage will apply every x seconds
    bool isInCooldown = false;

    private void Start()
    {
        OnHealthUpdated();
    }

    public override void OnHealthUpdated()
    {
        if (Health == 3)
        {
            imgHealthBar.rectTransform.sizeDelta = new Vector2(382, 120);
        }
        else if (Health == 2)
        {
            imgHealthBar.rectTransform.sizeDelta = new Vector2(250, 120);
        }
        else if (Health == 1)
        {
            imgHealthBar.rectTransform.sizeDelta = new Vector2(125, 120);
        }
        else if (Health == 0)
        {
            imgHealthBar.rectTransform.sizeDelta = new Vector2(5, 120);
        }
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
        if (otherObject.CompareTag("mainEnemy"))
        {
            MainEnemyController mainEnemy = otherObject.GetComponent<MainEnemyController>();
            int amount = mainEnemy.Damage;
            SubtractHealth(amount);
            Instantiate(Blood, transform.position, Quaternion.identity);
        }

        if (otherObject.CompareTag("Kill"))
        {
            int amount = 1;
            SubtractHealth(amount);
            Instantiate(Blood, transform.position, Quaternion.identity);
        }

        if (otherObject.CompareTag("InstantKill"))
        {
            int amount = 3;
            SubtractHealth(amount);
            Instantiate(Blood, transform.position, Quaternion.identity);
        }

        base.HandleCollision(otherObject);
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

    public void SubtractHealth(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OnDeath();
        }

        OnHealthUpdated();
    }

    public override void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        base.OnDeath();
    }
}
