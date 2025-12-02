using UnityEngine;

public class Unstable_Platform_Script : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float countDownTime = 3f;
    private float countDown;

    [Header("Animation Settings")]
    [SerializeField] private Animator platformAnimator;
    [SerializeField] private int AnimationDenominator = 3;  // Shake begins at 1/AnimationDenominator

    [Header("References")]
    [SerializeField] private ParticleSystem dustCollapse;
    [SerializeField] private ParticleSystem dustRespawn;
    [SerializeField] private LayerMask playerLayer;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private bool isCounting = false;
    private bool shakePlayed = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        ResetPlatform();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            if (!isCounting)
                StartCoroutine(StartCountdown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            StopAllCoroutines();
            ResetPlatform();
        }
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        isCounting = true;
        shakePlayed = false;

        // Reset to idle
        platformAnimator.ResetTrigger("Shake");
        platformAnimator.SetTrigger("IDLE");

        countDown = countDownTime;

        while (countDown > 0f)
        {
            float shakeThreshold = countDownTime / (float)AnimationDenominator;

            if (!shakePlayed && countDown <= shakeThreshold)
            {
                shakePlayed = true;
                platformAnimator.SetTrigger("Shake");
            }

            yield return new WaitForSeconds(1f);
            countDown -= 1f;
        }

        CollapsePlatform();

        // Wait before respawn
        yield return new WaitForSeconds(countDownTime);

        RestorePlatform();
        ResetPlatform();
    }

    private void CollapsePlatform()
    {
        // Play particles
        if (dustCollapse) dustCollapse.Play();

        // Disable platform
        col.enabled = false;
        spriteRenderer.enabled = false;
    }

    private void RestorePlatform()
    {
        // Play respawn particles
        if (dustRespawn) dustRespawn.Play();

        col.enabled = true;
        spriteRenderer.enabled = true;
    }

    private void ResetPlatform()
    {
        countDown = countDownTime;
        shakePlayed = false;
        isCounting = false;

        platformAnimator.ResetTrigger("Shake");
        platformAnimator.SetTrigger("IDLE");
    }
}
