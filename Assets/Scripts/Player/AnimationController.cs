using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
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
            anim.SetInteger("Attack", 0);
        }
    }
}
