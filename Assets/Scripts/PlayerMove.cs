using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // Movement variables
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Animation variable
    private Animator animator;

    //Footsteps sounds
    private bool playingFootsteps = false;
    private float footstepSpeed = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        //Stop player movement when pause
        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            //Stop footsteps
            StopFootsteps();
            return;
        }

        // Move the player
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetBool("isWalking",rb.linearVelocity.magnitude > 0);

        //Start Footsteps
        if(rb.linearVelocity.magnitude > 0 && !playingFootsteps)
        {
            StartFootsteps();
        }
        else if(rb.linearVelocity.magnitude == 0)
        {
            StopFootsteps();
        }
        // Check if player is moving
        if (moveInput != Vector2.zero)
        {

            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);

            // Save last input direction for Idle animation
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        else
        {
            animator.SetBool("isWalking", false);

            // Force the Idle animation by using the last direction
            animator.SetFloat("InputX", animator.GetFloat("LastInputX"));
            animator.SetFloat("InputY", animator.GetFloat("LastInputY"));
        }
    }

    // Handle movement input
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (context.canceled)
        {
            moveInput = Vector2.zero; // Stops Rigidbody movement
        }
    }
    void StartFootsteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
        
    }
    void StopFootsteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("Footsteps",true);
    }

        
}
