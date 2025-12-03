using UnityEngine;

public class ActivationaAreaLogic3 : MonoBehaviour
{
    [SerializeField] private FinalCauseActions boss;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip finalBossMusic;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = true;
            boss.startParticleSystem();
            SoundManager.Instance.changeMusic(finalBossMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = false;
            boss.stopParticleSystem();
        }
    }
}
