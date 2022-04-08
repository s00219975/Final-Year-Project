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
    public GameObject DeathPanel;

    //set a cooldown for some time, thus damage will apply every x seconds
    bool isInCooldown = false;

    private void Start()
    {
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

    public void DeathPanelActive()
    {
        DeathPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public override void OnDeath()
    {
        Invoke("DeathPanelActive", 2);

        base.OnDeath();
    }
}
