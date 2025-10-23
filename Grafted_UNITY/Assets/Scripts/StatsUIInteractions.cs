using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StatsUIInteractions : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Image speedBar;
    [SerializeField] private Image strengthBar;
    [SerializeField] private Canvas StatsUI;
    [SerializeField] private Canvas PauseUI;
    private bool activeStats = false;
    private bool activePause = false;

    // Listen to when the player or the boss gets hit
    void Start()
    {
        PlayerInteractions.onPlayerHitUI += PlayerInteractions_updateLife;
        PlayerInteractions.onPlayerSpeedChange += PlayerInteractions_updateSpeed;
        PlayerInteractions.onPlayerStrengthChange += PlayerInteractions_updateStrength;
    }

    // Adjust the player health bar in the UI
    private void PlayerInteractions_updateLife(object sender, System.EventArgs e)
    {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerHealth = player.health;

        lifeBar.fillAmount = playerHealth;
    }

    private void PlayerInteractions_updateSpeed(object sender, System.EventArgs e)
    {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerSpeed= player.attackCooldown;

        speedBar.fillAmount = playerSpeed;
    }

    private void PlayerInteractions_updateStrength(object sender, System.EventArgs e)
    {
        PlayerInteractions player = (PlayerInteractions)sender;
        float playerStrength= player.attackPower;

        strengthBar.fillAmount = playerStrength;
    }

    public void Stats()
    {
        if (activeStats)
        {
            activeStats = false;
            StatsUI.gameObject.SetActive(activeStats);
        }
        else
        {
            activeStats = true;
            StatsUI.gameObject.SetActive(activeStats);
            activePause = false;
            PauseUI.gameObject.SetActive(activePause);
        }
        
    }

    public void Pause()
    {
        if (activePause)
        {
            activePause = false;
            PauseUI.gameObject.SetActive(activePause);
        }
        else
        {
            activeStats = false;
            StatsUI.gameObject.SetActive(activeStats);
            activePause = true;
            PauseUI.gameObject.SetActive(activePause);
        }
    }
       
}
