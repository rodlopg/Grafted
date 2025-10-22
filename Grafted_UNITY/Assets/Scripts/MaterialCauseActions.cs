using JetBrains.Annotations;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MaterialCauseActions : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform projectileSpawnLocation;

    // Boss stats
    public float health { get; private set; }
    private float attackCooldown = 2f;

    public static event EventHandler onBossHitUI;

    void Start()
    {
        health = 1f;
    }

    // Projectile attack every 3 seconds
    void Update() {
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

        if (this.health < 0) {
            death();
        }
    }

    public void death() {
        SpawnBodyPart.SpawnRandomBodyPart(gameObject.transform.position);
        Destroy(gameObject);
    }

    // Spawn the projectile and give it the player's location
    private void projectileAttack() {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
        projectile.GetComponent<MaterialCauseProjectileLogic>().playerTransform = playerTransform;
    }
}
