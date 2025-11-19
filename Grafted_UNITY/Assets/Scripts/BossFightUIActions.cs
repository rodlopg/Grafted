using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Actions;
using static GameProvider;

public class BossFightUIActions : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private Image[] Vitruvian;
    // Maps each limb to the corresponding graft action
    public static Dictionary<PlayerLimb, Image> VitruvianTranslator;

    // Listen to when the player or the boss gets hit
    void Start()
    {
        VitruvianTranslator = new Dictionary<PlayerLimb, Image>
    {
        { PlayerLimb.Head, Vitruvian[0] },
        { PlayerLimb.Torso, Vitruvian[1] },
        { PlayerLimb.Left_Arm, Vitruvian[2] },
        { PlayerLimb.Right_Arm, Vitruvian[3] },
        { PlayerLimb.Left_Leg, Vitruvian[4] },
        { PlayerLimb.Right_Leg, Vitruvian[5] },
    };

        PlayerInteractions.onPlayerHitUI += PlayerInteractions_onPlayerHitUI;
        MaterialCauseActions.onBossHitUI += MaterialCauseActions_onBossHitUI;
        GameProvider.onBodyPartDetection += GameProvider_onBodyPartDetection;
    }

    // Adjust the boss health bar in the UI
    private void MaterialCauseActions_onBossHitUI(object sender, System.EventArgs e) {
        bossHealthBar.enabled = true;

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

    private void GameProvider_onBodyPartDetection(object sender, BodyPartEventArgs e)
    {
        VitruvianTranslator[e.Slot].color = Color.green;
    }

}
