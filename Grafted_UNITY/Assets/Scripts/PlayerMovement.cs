using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // New input system object
    private PlayerInputActions playerInputActions;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask groundLayer;

    private float moveSpeed = 5f;
    private float jumpForce = 10f;
    private float dashForce = 15f;
    private float dashDuration = 0.2f;

    private bool isDashing = false;
    private bool isJumping = false;
    private bool canDash = false;

    private float groundCheckDistance = 0.87f;

    // This timer prevents double collisions
    private float raycastTimer = 0.2f;

    private Vector2 moveDirection, lastDirection;

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

        // If the ray hits, then the player can dash
        if (hit && !isDashing) {
            canDash = true;
        } 
        
        // If the ray hits and the timer is done, then the player can jump
        if (hit && raycastTimer < 0) {
            isJumping = false;
            raycastTimer = 0.2f;
        }
    }

    private void Update() {
        // If the player is not dashing, adjust position as normal with the moveDirection vector
        if (!isDashing) {
            rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
        }

        // If the player is jumping, lower the timer
        if (isJumping) {
            raycastTimer -= Time.deltaTime;
        }
    }

    // Jump event
    public void Jump(InputAction.CallbackContext context) {
        // If the player is not already jumping, apply the upward force
        if (context.performed && !isJumping) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
    }

    // Dash event
    public void Dash(InputAction.CallbackContext context) {
        // If the player has touched the floor so their dash is available (canDash), apply the dash coroutine
        if (context.performed && canDash && !isDashing) {
            canDash = false;
            StartCoroutine(dashRoutine());
        }
    }

    // Movement event
    public void Movement(InputAction.CallbackContext context) {
        // Read Vector2 from Input System
        moveDirection = context.ReadValue<Vector2>();

        // As long as the last move direction isn't 0, it will get stored so that the dash can be made even when no direction keys are being pressed
        if (moveDirection != Vector2.zero) {
            lastDirection = moveDirection;
        }
    }

    // Dash coroutine
    private IEnumerator dashRoutine() {
        isDashing = true;
        canDash = false;

        // Save the previous gravity and set the current to 0 so only the dash force is applied
        float prevGravity = rb.gravityScale;
        rb.gravityScale = 0;
        tr.emitting = true;
        
        rb.linearVelocity = new Vector2(lastDirection.x * dashForce, lastDirection.y * dashForce);

        yield return new WaitForSeconds(dashDuration);

        // Enable previous gravity
        rb.gravityScale = prevGravity;
        tr.emitting = false;

        isDashing = false;
    }
}