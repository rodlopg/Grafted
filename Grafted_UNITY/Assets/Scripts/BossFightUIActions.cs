using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Actions;
using static GameProvider;
using static PlayerState;
using Limb = Actions.PlayerLimb;

public class BossFightUIActions : MonoBehaviour
{
    [SerializeField] private PlayerState P_State;
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private Image E_Key;
    [SerializeField] private Image[] Vitruvian;
    // Maps each limb to the corresponding graft action
    public static Dictionary<PlayerLimb, Image> VitruvianTranslator;
    private Limb uLastLimb = Limb.NULL;

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
        PlayerState.onBodyPartDetection += PlayerState_onBodyPartDetection;
        PlayerInteractions.onPlayerHealUI += PlayerInteractions_onPlayerHealUI;
    }

    void OnDestroy() {
        PlayerInteractions.onPlayerHitUI -= PlayerInteractions_onPlayerHitUI;
        MaterialCauseActions.onBossHitUI -= MaterialCauseActions_onBossHitUI;
        PlayerState.onBodyPartDetection -= PlayerState_onBodyPartDetection;
        PlayerInteractions.onPlayerHealUI -= PlayerInteractions_onPlayerHealUI;
    }

    private void PlayerInteractions_onPlayerHealUI(object sender, System.EventArgs e)
    {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerHealth = player.health;

        playerHealthBar.fillAmount = playerHealth;
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

    private void PlayerState_onBodyPartDetection(object sender, BodyPartEventArgs e)
    {
        // 1. If no limb detected → reset ALL to white
        if (e.Slot == Limb.NULL)
        {
            foreach (var img in VitruvianTranslator.Values)
                img.color = Color.white;

            uLastLimb = Limb.NULL;
            Hide_E_Key();
            return;
        }

        // 2. Reset ALL to white first
        foreach (var img in VitruvianTranslator.Values)
            img.color = Color.white;

        // 3. Paint only the closest limb in green
        VitruvianTranslator[e.Slot].color = Color.green;
        Show_E_Key();

        // 4. Remember last limb
        uLastLimb = e.Slot;
    }

    public Process Show_E_Key()
    {
        E_Key.gameObject.SetActive(true);

        return Process.DONE;
    }

    public Process Hide_E_Key()
    {
        E_Key.gameObject.SetActive(false);

        return Process.DONE;
    }

}
