using UnityEngine;

using Process = Actions.Process;
using P_Action = Actions.PlayerAction;
public class GameProvider : MonoBehaviour
{
    [SerializeField] GameState G_State;
    [SerializeField] PlayerState P_State;
    private Process SetLastPlayerAction(P_Action action)
    {
        G_State.LastPlayerAction = action;
        return Process.DONE;
    } 

    public Process GraftLimb(BodyPart limb)
    {
        if(P_State.GraftLimb(limb) == Process.DONE)
        {
            SetLastPlayerAction(limb.GetAction());
        }
        
        return Process.DONE;
    }
}
