using UnityEngine;

public class DamageStalactite : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private float stalactiteDamage = 0.05f;

    private void OnCollisionEnter2D(Collision2D collision) {
        // To theck for the same layer mask as the player's and applly damage to them
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            PlayerInteractions playerInteraction = collision.gameObject.GetComponent<PlayerInteractions>();
            playerInteraction.takeDamage(stalactiteDamage);
        }
    }
}
