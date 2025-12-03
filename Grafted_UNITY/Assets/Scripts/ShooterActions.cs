using UnityEngine;

public class ShooterActions : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnLocation;
    [SerializeField] private float shootCooldown = 2f;

    [Header("Player Detection")]
    [SerializeField] private LayerMask playerLayer;
    private Transform playerTransform;

    [Header("Animations (Optional)")]
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private string shootTriggerName = "Shoot";

    private float cooldownTimer;
    private bool playerInRange = false;

    private void Start()
    {
        cooldownTimer = shootCooldown;

        // Automatically find the player in the scene
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("ShooterActions: No GameObject with tag 'Player' was found in the scene.");
        }
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (playerTransform == null) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            ShootProjectile();
            cooldownTimer = shootCooldown;
        }
    }

    private void ShootProjectile()
    {
        if (enemyAnimator != null && !string.IsNullOrEmpty(shootTriggerName))
        {
            enemyAnimator.SetTrigger(shootTriggerName);
        }

        GameObject projectile = Instantiate(
            projectilePrefab,
            projectileSpawnLocation.position,
            projectileSpawnLocation.rotation
        );

        if (projectile.TryGetComponent(out MaterialCauseProjectileLogic logic))
        {
            logic.playerTransform = playerTransform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            playerInRange = false;
        }
    }
}
