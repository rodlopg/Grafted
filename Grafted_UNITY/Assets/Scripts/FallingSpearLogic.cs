using UnityEngine;

public class FallingSpearLogic : MonoBehaviour {
    [SerializeField] private float fallSpeed = 8f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioSource audioSource;

    private float targetX;     // Player X snapshot at fire time
    private float damage = 0.3f;

    private void Start() {
        // Lock X position permanently
        transform.rotation = Quaternion.Euler(0, 0, -90f);
        Vector3 pos = transform.position;
        pos.x = targetX;       
        transform.position = pos;

        audioSource.Play();
        Destroy(gameObject, 5f);
    }

    private void Update() {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    public void SetTargetX(float x) {
        targetX = x;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0) {
            collision.gameObject.GetComponent<PlayerInteractions>().takeDamage(damage);
        }

        Destroy(gameObject);
    }
}
