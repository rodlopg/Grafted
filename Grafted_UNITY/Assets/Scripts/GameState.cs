using UnityEngine;
using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

[CreateAssetMenu(menuName = "Context/GameState")]
public class GameState : ScriptableObject
{
    private PlayerState PlayerState;
    private Actions.PlayerAction LastPlayerAction;

    public Process SetLastPlayerAction(P_Action action)
    {
        this.LastPlayerAction = action;
        return Process.DONE;
    }
}
