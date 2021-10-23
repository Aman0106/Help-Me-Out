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
    float slideVelocity;

    [Header("Controls")]
    [SerializeField] float jumpStregth = 5f;
    [SerializeField] float speed = 2f;
    [SerializeField] float wallSlideSpeed = 0.1f;
    [SerializeField] float wallSlideMaxspeed = 4f;

    [Header("Collsions Checks")]
    [SerializeField] float groundcheckDepth = 0.1f;
    [SerializeField] float groundcheckLength = 0.5f;
    [SerializeField] float wallcheckLength = 0.1f;
    [SerializeField] float wallcheckHieght = 0.9f;

    [Header("Graphics")]
    [SerializeField] float jumpParticlesDelay = 0.02f;

    [Header("Refrences")]
    [SerializeField] Transform groundcheck;
    [SerializeField] Transform wallcheck;
    [SerializeField]BoxCollider2D selfCollider;
    [SerializeField] LayerMask jumpable;
    [SerializeField] ParticleSystem runParticles;
    [SerializeField] ParticleSystem wallslideParticles;
    [SerializeField] GameObject jumpParticleSystem;

    enum PlayerState{Idle, inAir, Running}
    PlayerState currentPlayerState;


    void Awake()
    {
        animator = GetComponent<Animator>();

        controls = new PlayerInputActions();
        EnableDissable(true);

        controls.PlayerActions.Jump.performed += Jump;
        controls.PlayerActions.Movement.performed += Move;

        rb = GetComponent<Rigidbody2D>();
        
        selfCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        WallSlide();
        SetPlayerState();
        RunAnim();

    }

    void Move(InputAction.CallbackContext ctx){
        // transform.Rotate(new Vector2(0, 180f * controls.PlayerActions.Movement.ReadValue<float>()));
    }

    void Movement(){
        float xvelocity = 0;
        if(!WallCheck())
            xvelocity = controls.PlayerActions.Movement.ReadValue<float>() * speed;
        velocity = new Vector2(xvelocity, rb.velocity.y);
    }

    void WallSlide(){
        if(WallCheck() && rb.velocity.y <= 0){
            slideVelocity -= wallSlideSpeed * Time.deltaTime;
            slideVelocity = Mathf.Clamp(slideVelocity, -wallSlideMaxspeed, rb.velocity.y );
            velocity.y = slideVelocity;
            // wallslideParticles.Simulate(1);

        }else{
            slideVelocity = 0;
            // wallslideParticles.Simulate(0);
        }
    }

    void RunAnim(){
        animator.SetBool("Running", currentPlayerState == PlayerState.Running);
        if(currentPlayerState == PlayerState.Running){
            // runParticles.duration = 1;
        }
        else{
            // runParticles.duration =;
        }
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
        bool jump = Physics2D.OverlapBox(groundcheck.position, new Vector2(groundcheckLength,groundcheckDepth), 0, jumpable);
        if(jump && currentPlayerState == PlayerState.inAir)
            animator.SetTrigger("Squash");
        return jump;
    }

    bool WallCheck(){
        if(!CanJump()){
            bool isWall = Physics2D.OverlapBox(wallcheck.position, new Vector2(wallcheckLength, wallcheckHieght), 0, jumpable) || Physics2D.OverlapBox(new Vector2(wallcheck.position.x - 0.85f, wallcheck.position.y), new Vector2(wallcheckLength, wallcheckHieght), 0, jumpable);
            return  isWall;
        }

        return false;
    }

    private void FixedUpdate() {
        rb.velocity = velocity;

    }

    public void Jump(InputAction.CallbackContext ctx){
        if(!CanJump())
            return;
        rb.velocity = new Vector2(rb.velocity.x,jumpStregth);
        animator.SetTrigger("Squish");
        StartCoroutine(SpawnJumpParticle(jumpParticlesDelay));
    }

    IEnumerator SpawnJumpParticle(float time){
        yield return new WaitForSeconds(time);

        GameObject.Instantiate(jumpParticleSystem, groundcheck.position, Quaternion.Euler(Vector2.up));
    }

    public void EnableDissable(bool val){
        if (val)
            controls.PlayerActions.Enable();
        else
            controls.PlayerActions.Disable();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundcheck.position, new Vector3(groundcheckLength, groundcheckDepth, 0.1f));

        Gizmos.DrawCube(wallcheck.position, new Vector3(wallcheckLength , wallcheckHieght, 0.1f));
        Gizmos.DrawCube(new Vector2(wallcheck.position.x - 0.85f, wallcheck.position.y), new Vector3(wallcheckLength, wallcheckHieght, 0.1f));
    }

}
