using System;
using UnityEngine;

public class MaterialCauseProjectileLogic : MonoBehaviour, IDamageable
{
    public Transform playerTransform;

    [SerializeField] private LayerMask playerLayer;

    private float projectileSpeed = 2.5f;
    private float projectileDamage = 0.1f;
    private float projectileHealth;

    private void Start() {
        // The projectile is one-shot from death
        projectileHealth = 0.01f;
    }

    void Update()
    {
        if (playerTransform == null) return;

        Vector3 moveDir = (playerTransform.position - transform.position).normalized;
        transform.position += moveDir * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            PlayerInteractions playerInteraction = collision.gameObject.GetComponent<PlayerInteractions>();
            playerInteraction.takeDamage(projectileDamage);
            Destroy(gameObject);
        }

    }

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
