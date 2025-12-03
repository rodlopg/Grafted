using UnityEngine;

public class ActivationaAreaLogic2 : MonoBehaviour
{
    [SerializeField] private FormalCauseActions boss;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip formalBossMusic;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = true;
            boss.startParticleSystem();
            SoundManager.Instance.changeMusic(formalBossMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = false;
            boss.stopParticleSystem();
        }
    }
}
