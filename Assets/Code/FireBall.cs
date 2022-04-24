using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float movementSpeed = 10;
    public int Damage = 1;

    public void SetDirection(Vector3 direction)
    {
        transform.up = direction.normalized;
        GetComponent<Rigidbody2D>().velocity = transform.up * movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
