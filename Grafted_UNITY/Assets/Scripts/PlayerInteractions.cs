using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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
    private float attackPower = 0.05f;
    private float attackCooldown = 0.5f;
    private bool canAttack = true;

    // Event that fires when player gets hit
    public static event EventHandler onPlayerHitUI;
            
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
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0, attackableLayer);

            // Apply damage to all attackable objects
            for (int i = 0; i < hits.Length; i++) {
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
}
