using UnityEngine;

public class ActivationaAreaLogic : MonoBehaviour
{
    [SerializeField] private EfficientCauseActions boss;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip efficientBossMusic;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = true;
            boss.startParticleSystem();
            SoundManager.Instance.changeMusic(efficientBossMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = false;
            boss.stopParticleSystem();
        }
    }
}
