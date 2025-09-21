using UnityEngine;
using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

public class GameState : MonoBehaviour
{
    private PlayerState PlayerState;
    private Actions.PlayerAction LastPlayerAction;

    public Process SetLastPlayerAction(P_Action action)
    {
        this.LastPlayerAction = action;
        return Process.DONE;
    }
}
