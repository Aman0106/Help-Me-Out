using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{

    PlayerInputActions controls;
    Rigidbody2D rb;
    Animator animator;
    
    Vector2 velocity;
    [Header("Controls")]
    [SerializeField] float jumpStregth = 5f;
    [SerializeField] float speed = 2f;
    [SerializeField] LayerMask jumpable;

    [Header("Refrences")]
    [SerializeField] Transform groundcheck;

    enum PlayerState{Idle, inAir, Running}
    PlayerState currentPlayerState;

    BoxCollider2D selfCollider;

    void Awake()
    {
        animator = GetComponent<Animator>();

        controls = new PlayerInputActions();
        controls.PlayerActions.Enable();

        controls.PlayerActions.Jump.performed += Jump;
        controls.PlayerActions.Movement.performed += Move;

        rb = GetComponent<Rigidbody2D>();
        
        selfCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector2(controls.PlayerActions.Movement.ReadValue<float>() * speed, rb.velocity.y);

        SetPlayerState();
        RunAnim();

    }

    void Move(InputAction.CallbackContext ctx){

    }

    void RunAnim(){
        animator.SetBool("Running", currentPlayerState == PlayerState.Running);
        print(CanJump());
    }

    void SetPlayerState(){
        if(!CanJump())
            currentPlayerState = PlayerState.inAir;
        else if(velocity.x != 0)
            currentPlayerState = PlayerState.Running;
        else
            currentPlayerState = PlayerState.Idle;
    }

    bool CanJump(){
        bool jump = Physics2D.OverlapBox(groundcheck.position, new Vector2(selfCollider.size.x, 0.1f), 0, jumpable);
        if(jump && currentPlayerState == PlayerState.inAir)
            animator.SetTrigger("Squash");
        return jump;
    }

    private void FixedUpdate() {
        // rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
        rb.velocity = velocity;

    }

    public void Jump(InputAction.CallbackContext ctx){
        if(!CanJump())
            return;
        rb.velocity = new Vector2(rb.velocity.x,jumpStregth);
        animator.SetTrigger("Squish");
    }

}
