using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConroller : MonoBehaviour
{
    private void Start()
    {
        OnHealthUpdated();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }    

    public virtual void OnDeath() { }
    public virtual void OnHealthUpdated() { }
    public virtual void HandleCollision(GameObject otherObject) { }
}
