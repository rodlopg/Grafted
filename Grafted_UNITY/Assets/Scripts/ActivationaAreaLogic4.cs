using UnityEngine;

public class ActivationaAreaLogic4 : MonoBehaviour
{
    [SerializeField] private MaterialCauseActions boss;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip materialBossMusic;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = true;
            SoundManager.Instance.changeMusic(materialBossMusic);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            boss.playerInRange = false;
        }
    }
}
