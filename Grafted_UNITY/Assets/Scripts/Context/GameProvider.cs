using UnityEngine;

using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;
public class GameProvider : MonoBehaviour
{
    [SerializeField] GameState G_State;
    [SerializeField] PlayerState P_State;

    public Process Provide_Graft(Scriptable_BodyPart limb)
    {
        if(P_State.GraftLimb(limb) == Process.DONE)
        {
            G_State.SetLastPlayerAction(limb.GetAction());
        }
        
        return Process.DONE;
    }

   
}
