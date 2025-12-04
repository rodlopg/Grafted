using UnityEngine;
using System.Collections;

public class Unstable_Platform_Script : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float countDownTime = 1f; // Time before platform disappears
    [SerializeField] private float respawnTime = 1f; // Time until platform reappears
    private float countDown;

    [Header("Platform Reference")]
    [Tooltip("Drag the child platform GameObject here (the one with sprite/collider/animator)")]
    [SerializeField] private GameObject platformObject;

    [Header("Animation Settings")]
    [SerializeField] private Animator platformAnimator;
    [SerializeField] private int AnimationDenominator = 3;  // Shake begins at 1/AnimationDenominator

    [Header("References")]
    [SerializeField] private ParticleSystem dustCollapse;
    [SerializeField] private ParticleSystem dustRespawn;
    [SerializeField] private LayerMask playerLayer;

    private Collider2D platformCollider;
    private SpriteRenderer[] platformSprites; // All sprites in platform hierarchy
    private bool isCounting = false;
    private bool shakePlayed = false;

    private void Awake()
    {
        // Auto-find platform if not assigned
        if (platformObject == null && transform.childCount > 0)
        {
            platformObject = transform.GetChild(0).gameObject;
        }

        // Get all sprite renderers in platform and its children
        if (platformObject != null)
        {
            platformSprites = platformObject.GetComponentsInChildren<SpriteRenderer>();
            platformCollider = platformObject.GetComponent<Collider2D>();
        }
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

    private IEnumerator StartCountdown()
    {
        isCounting = true;
        shakePlayed = false;

        // Reset to idle
        if (platformAnimator)
        {
            platformAnimator.ResetTrigger("Shake");
            platformAnimator.SetTrigger("IDLE");
        }

        countDown = countDownTime;

        while (countDown > 0f)
        {
            float shakeThreshold = countDownTime / (float)AnimationDenominator;

            if (!shakePlayed && countDown <= shakeThreshold && platformAnimator)
            {
                shakePlayed = true;
                platformAnimator.SetTrigger("Shake");
            }

            yield return new WaitForSeconds(1f);
            countDown -= 1f;
        }

        // Collapse platform
        CollapsePlatform();

        // Wait for respawn time
        yield return new WaitForSeconds(respawnTime);

        // Restore platform
        RestorePlatform();
        ResetPlatform();
    }

    private void CollapsePlatform()
    {
        // Play collapse particles
        if (dustCollapse) dustCollapse.Play();

        // Disable all sprites in the platform hierarchy
        if (platformSprites != null)
        {
            foreach (SpriteRenderer sprite in platformSprites)
            {
                if (sprite != null)
                    sprite.enabled = false;
            }
        }

        // Disable platform collider
        if (platformCollider) platformCollider.enabled = false;
    }

    private void RestorePlatform()
    {
        // Enable platform collider
        if (platformCollider) platformCollider.enabled = true;

        // Enable all sprites in the platform hierarchy
        if (platformSprites != null)
        {
            foreach (SpriteRenderer sprite in platformSprites)
            {
                if (sprite != null)
                    sprite.enabled = true;
            }
        }

        // Play respawn particles
        if (dustRespawn) dustRespawn.Play();
    }

    private void ResetPlatform()
    {
        countDown = countDownTime;
        shakePlayed = false;
        isCounting = false;

        // Make sure platform is visible
        if (platformCollider) platformCollider.enabled = true;
        if (platformSprites != null)
        {
            foreach (SpriteRenderer sprite in platformSprites)
            {
                if (sprite != null)
                    sprite.enabled = true;
            }
        }

        if (platformAnimator)
        {
            platformAnimator.ResetTrigger("Shake");
            platformAnimator.SetTrigger("IDLE");
        }
    }
}