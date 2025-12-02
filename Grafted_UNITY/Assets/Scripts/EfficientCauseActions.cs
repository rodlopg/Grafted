using System;
using UnityEngine;
using E_Action = Actions.EnemyAction;
using G_Provider = GameProvider;

public class EfficientCauseActions : MonoBehaviour, IDamageable {
    [SerializeField] private GameObject projectilePrefab;        
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform projectileSpawnLocation;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private ParticleSystem arenaParticles;      

    public float health { get; private set; }
    private float attackCooldown = 2f;

    public static event EventHandler onBossHitUI;
    public static event EventHandler onBossDeathUI;

    private bool playerInRange = false;

    void Start() {
        health = 2f;
        arenaParticles.Stop();
    }

    void Update() {
        if (!playerInRange) return;

        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) {
            projectileAttack();
            attackCooldown = 2f;
        }
    }

    public void takeDamage(float damage) {
        this.health -= damage;
        onBossHitUI?.Invoke(this, EventArgs.Empty);

        if (health <= 0f) {
            death();
        } else if (G_Provider.Animate(enemyAnimator, E_Action.Take_Damage) == Actions.Process.DONE)
            return;
    }

    public void death() {
        SpawnBodyPart.SpawnRandomBodyPart(transform.position);
        Destroy(gameObject);
    }

    // Shoot the snowball projectile
    private void projectileAttack() {
        GameObject projectile =
            Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);

        // Assign the player reference to the snowball script
        projectile.GetComponent<HorizontalCauseProjectileLogic>().playerTransform = playerTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            playerInRange = true;
            arenaParticles.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            playerInRange = false;
            attackCooldown = 2f;
            arenaParticles.Stop();
        }
    }
}