using System;
using UnityEngine;

public class MaterialCauseProjectileLogic : MonoBehaviour, IDamageable
{
    public Transform playerTransform;

    [SerializeField] private LayerMask playerLayer;

    // Projectiles stats
    private float projectileSpeed = 2.5f;
    private float projectileDamage = 0.3f;
    private float projectileHealth;

    private void Start() {
        // The projectile is one-shot from death
        projectileHealth = 0.01f;
    }

    void Update()
    {
        // If the projectile does not know the player's location, it won't do anything
        if (playerTransform == null) return;

        Vector3 moveDir = (playerTransform.position - transform.position).normalized;
        transform.position += moveDir * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // To theck for the same layer mask as the player's and applly damage to them
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            PlayerInteractions playerInteraction = collision.gameObject.GetComponent<PlayerInteractions>();
            playerInteraction.takeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

    // Damage taken and death functions for this projectile
    public void takeDamage(float damage) {
        projectileHealth -= damage;
       
        if (projectileHealth < 0) {
            death();
        }
    }

    public void death() {
        Destroy(gameObject);
    }
}
