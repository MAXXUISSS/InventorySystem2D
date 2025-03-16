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
            return;
        }

        // Move the player
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetBool("isWalking",rb.linearVelocity.magnitude > 0);

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
}
