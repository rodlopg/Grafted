using JetBrains.Annotations;
using System;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using E_Action = Actions.EnemyAction;
using G_Provider = GameProvider;

public class MaterialCauseActions : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform projectileSpawnLocation;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private AudioClip efficientAreaMusic;

    // Boss stats
    public float health { get; private set; }
    private float attackCooldown = 2f;

    public static event EventHandler onBossHitUI;
    public static event EventHandler onBossDeathUI;

    public bool playerInRange = false;

    void Start()
    {
        health = 1f;
    }

    // Projectile attack every 3 seconds
    void Update() {
        if (!playerInRange) return;

        attackCooldown -= Time.deltaTime;
        if(attackCooldown < 0) {
            projectileAttack();
            attackCooldown = 2f;
        }
    }

    // Damage taken and death functions for the boss
    public void takeDamage(float damage) {
        this.health -= damage;
        onBossHitUI?.Invoke(this, EventArgs.Empty);
        

        if (this.health <= 0f) {
            death();
        }else if (G_Provider.Animate(enemyAnimator, E_Action.Take_Damage) == Actions.Process.DONE) return;
    }

    public void death() {
        SpawnBodyPart.SpawnRandomBodyPart(gameObject.transform.position);
        SoundManager.Instance.changeMusic(efficientAreaMusic);
        Destroy(gameObject);
    }

    // Spawn the projectile and give it the player's location
    private void projectileAttack() {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
        projectile.GetComponent<MaterialCauseProjectileLogic>().playerTransform = playerTransform;
    }
}
