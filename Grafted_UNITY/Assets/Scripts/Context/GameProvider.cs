using UnityEngine;

using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

public class GameProvider : MonoBehaviour
{
    // Reference to the global GameState (ScriptableObject)
    [SerializeField] private GameState G_State;
    // Reference to the PlayerState (MonoBehaviour managing current body parts)
    [SerializeField] private PlayerState P_State;

    // Handles grafting a new limb into the player
    public Process Provide_Graft(Scriptable_BodyPart limb)
    {
        // Try to graft the new limb onto the player
        if (P_State.GraftLimb(limb) == Process.DONE)
        {
            // If graft succeeded, update the GameState with the last action performed
            G_State.SetLastPlayerAction(limb.GetAction());
        }

        return Process.DONE; // Always returns DONE for now
    }
}
