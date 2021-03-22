using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (playerMovement.movementFinish)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    anim.SetInteger("Attack", 1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    anim.SetInteger("Attack", 2);
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    anim.SetInteger("Attack", 3);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.SetInteger("Attack", 4);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    anim.SetInteger("Attack", 5);
                }
                else if (Input.GetMouseButton(0))
                {
                    anim.SetInteger("Attack", 6);
                }
                else
                {
                    return;
                }
            }            
        }
        else
        {
            anim.SetInteger("Attack", 0);
        }

    }
}
