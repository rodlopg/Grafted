using JetBrains.Annotations;
using System;
using UnityEngine;

public class MaterialCauseActions : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform projectileSpawnLocation;

    public float health { get; private set; }
    private float attackCooldown = 3f;

    public static event EventHandler onBossHitUI;

    void Start()
    {
        health = 1f;
    }

    void Update() {
        attackCooldown -= Time.deltaTime;
        if(attackCooldown < 0) {
            projectileAttack();
            attackCooldown = 3f;
        }
    }

    public void takeDamage(float damage) {
        this.health -= damage;
        onBossHitUI?.Invoke(this, EventArgs.Empty);

        if (this.health < 0) {
            death();
        }
    }

    public void death() {
        Destroy(gameObject);
    }

    private void projectileAttack() {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnLocation);
        projectile.GetComponent<MaterialCauseProjectileLogic>().playerTransform = playerTransform;
    }
}
