using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour, IDamageable
{
    private const string ATTACK = "isAttackingRight";
    private const string HIT = "isHit";

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask attackableLayer;



    // Array with all hit objects
    private RaycastHit2D[] hits;

    // Entity attributes
    public float health { get; private set; }

    public float attackPower = 0.05f;

    public float attackCooldown = 0.5f;
    private bool canAttack = true;

    // Event that fires when player gets hit
    public static event EventHandler onPlayerHitUI;
    public static event EventHandler onPlayerSpeedChange;
    public static event EventHandler onPlayerStrengthChange;

    void Start()
    {
        health = 1f;
    }

    void Update()
    {
        // Attack cooldown logic
        if (!canAttack) {
            attackCooldown -= Time.deltaTime;

            if (attackCooldown < 0) {
                canAttack = true;
                attackCooldown = 0.5f;
            }
        }
    }

    // Attack handler
    public void Attack(InputAction.CallbackContext context) {
        if (context.performed && canAttack) {
            playerAnimator.SetTrigger(ATTACK);

            canAttack = false;

            // Hitbox 
            Vector2 attackDirection = playerMovement.getIsFacingRight() ? Vector2.right : Vector2.left;
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, attackDirection, 0, attackableLayer);

            // Apply damage to all attackable objects
            for (int i = 0; i < hits.Length; i++) {
                // If the collider is a trigger, its the deteciton radius, not the actual hitbox
                if (hits[i].collider.isTrigger) continue;

                IDamageable damagableObject = hits[i].collider.GetComponentInParent<IDamageable>();
                damagableObject.takeDamage(this.attackPower);
            }
        }
    }

    // Draw the hitbox in debug mode
    void OnDrawGizmosSelected() {
        if (attackTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    // Damage taken and death functions for the player
    public void takeDamage(float damage) {
        if(playerMovement.isDashing) return;

        this.health -= damage;
        playerAnimator.SetTrigger(HIT);
        onPlayerHitUI?.Invoke(this, EventArgs.Empty);

        if (this.health < 0) {
            death();
        }
    }

    public void death() {
        Destroy(gameObject);
    }

    public void changeSpeed(float speed) { 
        this.attackCooldown = speed;
        onPlayerSpeedChange?.Invoke(this, EventArgs.Empty);
    }

    public void changeStrength(float strength)
    {
        this.attackPower = strength;
        onPlayerStrengthChange?.Invoke(this, EventArgs.Empty);
    }


}
