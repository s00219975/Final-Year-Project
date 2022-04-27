using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CharacterState
{
    Idle = 0,
    Run = 1,
    Jump = 2,
    Dead = 3,
    Blinking = 4
}

public class Character : MonoBehaviour
{
    public PlayerHealth playerhealth;
    public CharacterState State;
    PlayerMovement playerMovement;
    Animator animator;

    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerhealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerhealth.Health == 0)
        {
            State = CharacterState.Dead;
            playerMovement.enabled = false;
            playerMovement.body.gravityScale = 3;
            playerMovement.body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (playerhealth.isDamaged == true)
        {
            State = CharacterState.Blinking;
        }
        else if(playerMovement.body.velocity.y > 0.1f || playerMovement.body.velocity.y < -0.1f)
        {
            State = CharacterState.Jump;
        }
        else if(playerMovement.body.velocity.x != 0)
        {
            State = CharacterState.Run;
        }
        else if (playerMovement.body.velocity.x == 0)
        {
            State = CharacterState.Idle;
        }

        animator.SetInteger("state", (int)State);
    }
}
