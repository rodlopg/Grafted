using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using P_Action = Actions.PlayerAction;

public class PlayerMovement : MonoBehaviour
{
    // New input system object
    private PlayerInputActions playerInputActions;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator playerAnimator;

    private float moveSpeed = 5f;
    private float jumpForce = 10f;
    private float dashForce = 15f;
    private float dashDuration = 0.2f;

    private bool isGrounded = false;
    public bool isDashing { get; private set; }
    private bool canDash = true;

    private float groundCheckDistance = 0.87f;

    // Default direction
    private Vector2 moveDirection;
    private Vector2 lastDirection = Vector2.right;

    private void Awake() {
        // Enabling the default/general controls
        playerInputActions = new PlayerInputActions();
        playerInputActions.General.Enable();    
    }

    private void FixedUpdate() {
        // Raycast to reset dash and jump
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Color rayColor = hit ? Color.green : Color.red;
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, rayColor);

        // If the ray hits, assume grounded state
        if (hit) {
            isGrounded = true;
            if (!isDashing) {
                canDash = true;
            }
        } else {
            isGrounded= false; 
        }
    }

    private void Update() {
        // If the player is not dashing, adjust position as normal with the moveDirection vector
        if (!isDashing) {
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
        }

        moveDirection = playerInputActions.General.Movement.ReadValue<Vector2>();

        // Store the last direction for dashing
        if (Mathf.Abs(moveDirection.x) > 0.01f)
        {
            lastDirection = new Vector2(Mathf.Sign(moveDirection.x), 0);
        }

        // Check if the player is moving
        bool isMovingLeft = moveDirection.x < -0.01f;
        bool isMovingRight = moveDirection.x > 0.01f;

        // Trigger appropriate animation
        if (isMovingLeft)
        {
            PlayerState.Animate(playerAnimator, P_Action.Left_Walk);
        }
        else if (isMovingRight)
        {
            PlayerState.Animate(playerAnimator, P_Action.Right_Walk);
        }
        else
        {
            PlayerState.Animate(playerAnimator, P_Action.Idle);
        }
    }

    // Jump event
    public void Jump(InputAction.CallbackContext context) {
        // If the player is grounded, apply the upward force
        if (context.performed && isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            if (PlayerState.Animate(playerAnimator, P_Action.Jump) == Actions.Process.DONE) return;
        }
    }

    // Dash event
    public void Dash(InputAction.CallbackContext context) {
        // If the player has touched the floor so their dash is available (canDash), apply the dash coroutine
        if (context.performed && !isDashing && canDash) {
            StartCoroutine(dashRoutine());
            if (PlayerState.Animate(playerAnimator, P_Action.Dash) == Actions.Process.DONE) return;
        }
    }

    // Movement event

    /*
    public void Movement(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

        // Store the last direction for dashing
        if (Mathf.Abs(moveDirection.x) > 0.01f)
        {
            lastDirection = new Vector2(Mathf.Sign(moveDirection.x), 0);
        }

        // Check if the player is moving
        bool isMovingLeft = moveDirection.x < -0.01f;
        bool isMovingRight = moveDirection.x > 0.01f;

        // Trigger appropriate animation
        if (isMovingLeft)
        {
            GameProvider.Provide_Animation(playerAnimator, P_Action.Left_Walk);
        }
        else if (isMovingRight)
        {
            GameProvider.Provide_Animation(playerAnimator, P_Action.Right_Walk);
        }
        else
        {
            GameProvider.Provide_Animation(playerAnimator, P_Action.Idle);
        }
    }
    */



    // Dash coroutine
    private IEnumerator dashRoutine() {
        isDashing = true;
        canDash = false;

        // Save the previous gravity and set the current to 0 so only the dash force is applied
        float prevGravity = rb.gravityScale;
        tr.emitting = true;

        Vector2 dashDirection;
        if (moveDirection == Vector2.zero) dashDirection = lastDirection;
         else dashDirection = moveDirection;

        if (dashDirection == Vector2.up) rb.gravityScale = prevGravity; 
        else rb.gravityScale = 0; 

        rb.linearVelocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);

        // Enable previous gravity
        rb.gravityScale = prevGravity;
        tr.emitting = false;
        isDashing = false;
    }
}