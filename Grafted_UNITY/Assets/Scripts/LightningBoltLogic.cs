using UnityEngine;

public class LightningPillarLogic : MonoBehaviour {
    [SerializeField] private Sprite warningSprite;
    [SerializeField] private Sprite strikeSprite;

    [SerializeField] private float warningTime = 0.7f;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float damage = 0.3f;

    private SpriteRenderer sr;
    private bool hasStruck = false;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = warningSprite;

        Invoke(nameof(Strike), warningTime);
    }

    private void Strike() {
        sr.sprite = strikeSprite;
        hasStruck = true;

        transform.localScale = new Vector3(5f, 10f, 1f);

        Vector3 pos = transform.position;
        pos.y = -27.82f;
        transform.position = pos;

        audioSource.Play();

        // Destroy after the sound finishes
        Destroy(gameObject, audioSource.clip.length);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!hasStruck) return;

        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            collision.GetComponent<PlayerInteractions>().takeDamage(damage);
        }
    }
}
