using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemyController : EnemyController
{
    public float movementSpeed = 4;
    public float AttackRange = 600;

    GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        base.Start();
    }

    private void Update()
    {
        transform.up = player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        body.MovePosition(transform.position + transform.up.normalized * movementSpeed * Time.deltaTime);
    }
}
