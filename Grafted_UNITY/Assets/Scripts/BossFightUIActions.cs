using UnityEngine;
using UnityEngine.UI;

public class BossFightUIActions : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image bossHealthBar;

    // Listen to when the player or the boss gets hit
    void Start()
    {
        PlayerInteractions.onPlayerHitUI += PlayerInteractions_onPlayerHitUI;
        MaterialCauseActions.onBossHitUI += MaterialCauseActions_onBossHitUI;
    }

    // Adjust the boss health bar in the UI
    private void MaterialCauseActions_onBossHitUI(object sender, System.EventArgs e) {
        MaterialCauseActions boss = (MaterialCauseActions)sender;
        float bossHealth = boss.health;

        bossHealthBar.fillAmount = bossHealth;
    }

    // Adjust the player health bar in the UI
    private void PlayerInteractions_onPlayerHitUI(object sender, System.EventArgs e) {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerHealth = player.health;

        playerHealthBar.fillAmount = playerHealth;
    }
}
