using System;
using UnityEngine;
using E_Action = Actions.EnemyAction;
using G_Provider = GameProvider;

public class FinalCauseActions : MonoBehaviour, IDamageable {
    [SerializeField] private GameObject lightningPrefab;
    [SerializeField] private Transform[] lightningPoints;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private ParticleSystem arenaParticles;
    [SerializeField] private AudioClip endingMusic;


    public void startParticleSystem() { arenaParticles.Play(); }
    public void stopParticleSystem() { arenaParticles.Stop(); }

    public float health { get; private set; }
    private float attackCooldown = 2f;

    public static event EventHandler onBossHitUI;
    public static event EventHandler onBossDeathUI;

    public bool playerInRange = false;

    void Start() {
        health = 0.4f;
        stopParticleSystem();
    }

    void Update() {
        if (!playerInRange) return;

        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0) {
            lightningAttack();
            attackCooldown = 2f;
        }
    }

    public void takeDamage(float damage) {
        this.health -= damage;
        onBossHitUI?.Invoke(this, EventArgs.Empty);

        if (health <= 0f) {
            death();
        } else if (G_Provider.Animate(enemyAnimator, E_Action.Take_Damage) == Actions.Process.DONE)
            return;
    }

    public void death() {
        SpawnBodyPart.SpawnRandomBodyPart(transform.position);
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.VictoryScreen);
        SoundManager.Instance.changeMusic(endingMusic);
    }

    // Shoots 3 prelocated lightning bolts
    private void lightningAttack() {
        int count = 3;

        for (int i = 0; i < count; i++) {
            int index = UnityEngine.Random.Range(0, lightningPoints.Length);
            Instantiate(lightningPrefab, lightningPoints[index].position, Quaternion.identity);
        }
    }
}