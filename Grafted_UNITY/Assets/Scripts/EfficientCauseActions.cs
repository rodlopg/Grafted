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
    [SerializeField] private AudioClip formalAreaMusic;

    public void startParticleSystem() { arenaParticles.Play(); }
    public void stopParticleSystem() { arenaParticles.Stop(); }

    public float health { get; private set; }
    private float attackCooldown = 2f;

    public static event EventHandler onBossHitUI;
    public static event EventHandler onBossDeathUI;

    public bool playerInRange = false;

    void Start() {
        health = 2f;
        stopParticleSystem();
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
        SoundManager.Instance.changeMusic(formalAreaMusic);
        Destroy(gameObject);
    }

    // Shoot the snowball projectile
    private void projectileAttack() {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);

        // Assign the player reference to the snowball script
        projectile.GetComponent<SnowballLogic>().playerTransform = playerTransform;
    }
}