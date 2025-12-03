using System;
using UnityEngine;

public class SnowballLogic : MonoBehaviour, IDamageable {
    public Transform playerTransform;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioSource audioSource;

    private float projectileSpeed = 8f;
    private float projectileDamage = 0.1f;
    private float projectileHealth;

    private void Start() {
        projectileHealth = 0.01f;

        if (playerTransform != null) {
            // Match the player's Y level at spawn
            Vector3 pos = transform.position;
            pos.y = playerTransform.position.y;
            transform.position = pos;
            audioSource.Play();
        }
    }

    private void Update() {
        transform.position += Vector3.left * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            PlayerInteractions playerInteraction = collision.gameObject.GetComponent<PlayerInteractions>();
            playerInteraction.takeDamage(projectileDamage);
            Destroy(gameObject);
        } else {
            // If it hits a wall/ground/etc, also destroy
            Destroy(gameObject);
        }
    }

    public void takeDamage(float damage) {
        projectileHealth -= damage;
        if (projectileHealth <= 0) death();
    }

    public void death() {
        Destroy(gameObject);
    }
}
