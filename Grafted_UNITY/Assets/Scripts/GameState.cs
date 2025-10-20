using UnityEngine;
using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

[CreateAssetMenu(menuName = "Context/GameState")]
public class GameState : ScriptableObject
{
    // Reference to the player state (not actively used here)
    private PlayerState PlayerState;

    // Last action the player performed
    private static Actions.PlayerAction LastPlayerAction;

    // Updates the last player action and returns DONE
    public static Process SetLastPlayerAction(P_Action action)
    {
        GameState.LastPlayerAction = action;
        return Process.DONE;
    }



}
