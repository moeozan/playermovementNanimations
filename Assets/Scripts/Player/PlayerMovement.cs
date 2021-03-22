using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 moveInput;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Camera cam;
    private Animator anim;
    [HideInInspector] public bool movementFinish;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    private Vector3 Calculate()
    {
        moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(0, Vector3.up) * cam.transform.forward;
        forward.y = 0;
        moveInput += forward * Input.GetAxis("Vertical");
        moveInput += cam.transform.right * Input.GetAxis("Horizontal");
        return moveInput;
    }

    private void Movement()
    {
        Calculate();
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (moveInput != Vector3.zero && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            controller.Move(moveInput * Time.deltaTime * playerSpeed);
            gameObject.transform.forward = moveInput;
            anim.SetInteger("Speed", 1);
            movementFinish = false;           
        }
        else
        {
            anim.SetInteger("Speed", 0);
            movementFinish = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetInteger("Speed", 2);
            movementFinish = false;
        }
        /*
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        */
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
