using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private const string ATTACK = "isAttackingRight";

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private LayerMask attackableLayer;

    // Array with all hit objects
    private RaycastHit2D[] hits;

    // Entity attributes
    public float health { get; private set; }
    private float attackPower = 0.1f;
    private float attackCooldown = 0.5f;
    private bool canAttack = true;

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
                // Deal damage function on enemies
            }
        }
    }

    // Draw the hitbox in debug mode
    void OnDrawGizmosSelected() {
        if (attackTransform == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
