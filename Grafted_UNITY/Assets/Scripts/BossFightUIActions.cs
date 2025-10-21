using UnityEngine;
using UnityEngine.UI;

public class BossFightUIActions : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image bossHealthBar;

    void Start()
    {
        PlayerInteractions.onPlayerHitUI += PlayerInteractions_onPlayerHitUI;
        MaterialCauseActions.onBossHitUI += MaterialCauseActions_onBossHitUI;
    }

    private void MaterialCauseActions_onBossHitUI(object sender, System.EventArgs e) {
        MaterialCauseActions boss = (MaterialCauseActions)sender;
        float bossHealth = boss.health;

        bossHealthBar.fillAmount = bossHealth;
    }

    private void PlayerInteractions_onPlayerHitUI(object sender, System.EventArgs e) {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerHealth = player.health;

        playerHealthBar.fillAmount = playerHealth;
    }
}
