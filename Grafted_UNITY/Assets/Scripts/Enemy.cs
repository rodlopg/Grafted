using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    // Enemy stats
    [SerializeField] private string enemyName;
    [SerializeField] private float health;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float roamSpeed;
    [SerializeField] float roamChangeTimer = 2f;
    [SerializeField] int roamDirection = 1;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private Transform playerTransform;

    // For NPC behavior
    private bool playerInRange = false;
    private bool canSeePlayer = false;
    private bool isFacingRight = true;
    private bool canAttack = true;

    void Update()
    {
        if (!canAttack) {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0) {
                canAttack = true;
                attackCooldown = 1f;
            }
        }

        if (playerInRange) {
            lineOfSight();
        }

        if (canSeePlayer) {
            chasePlayer();
        } else {
            roamMovement();
        }
    }

    // To damage the player we use Collision
    private void OnCollisionStay2D(Collision2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            if (canAttack) {
                PlayerInteractions playerInteraction = collision.gameObject.GetComponent<PlayerInteractions>();
                playerInteraction.takeDamage(attackPower);
                canAttack = false;
            }
        }
    }

    // For detection checks we use Trigger
    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            playerInRange = false;
            canSeePlayer = false;
        }
    }

    // Shoots a ray in the direction of the player and if it collides with something in the obstacle layer, it can't see the player
    private void lineOfSight() {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer | playerLayer);

        if (hit.collider != null) {
            if (((1 << hit.collider.gameObject.layer) & playerLayer) != 0) canSeePlayer = true;
            else canSeePlayer = false;
        }
    }

    // Goes horizontally towards the player
    private void chasePlayer() {
        if (playerTransform == null) return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x, rb.linearVelocityY) * chaseSpeed;
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight)) flip();
    }

    // Roaming left and right while it does not see the player
    private void roamMovement() {
        roamChangeTimer -= Time.deltaTime;
        if (roamChangeTimer <= 0f) {
            roamChangeTimer = 2f;
            roamDirection *= -1;
            flip();
        }

        rb.linearVelocity = new Vector2(roamDirection * roamSpeed, rb.linearVelocity.y);
    }

    public void takeDamage(float damage) {
        health -= damage;

        if(this.health <= 0) {
            death();
        }
    }

    public void death() {
        SpawnBodyPart.SpawnRandomBodyPart(gameObject.transform.position);
        Destroy(gameObject);    
    }

    // Flip the enemy and its animations
    private void flip() {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
