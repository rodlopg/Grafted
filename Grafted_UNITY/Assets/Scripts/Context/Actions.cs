using UnityEngine;
using System.Collections.Generic;

public class Actions : MonoBehaviour
{
    public enum Process
    {
        START,
        WAIT,
        DONE
    }

    public enum PlayerLimb
    {
        Head,
        Torso,
        Left_Arm,
        Right_Arm,
        Left_Leg,
        Right_Leg,
    }

    public enum PlayerAction
    {
        Idle,
        Right_Walk,
        Left_Walk,
        Right_Attack,
        Left_Attack,
        Jump,
        Graft_Head,
        Graft_Torso,
        Graft_Left_Arm,
        Graft_Right_Arm,
        Graft_Left_Leg,
        Graft_Right_Leg,
    }

    public static Dictionary<PlayerLimb, PlayerAction> ActionTranslator = new Dictionary<PlayerLimb, PlayerAction>
    {
        { PlayerLimb.Head, PlayerAction.Graft_Head },
        { PlayerLimb.Torso, PlayerAction.Graft_Torso },
        { PlayerLimb.Left_Arm, PlayerAction.Graft_Left_Arm },
        { PlayerLimb.Right_Arm, PlayerAction.Graft_Right_Arm },
        { PlayerLimb.Left_Leg, PlayerAction.Graft_Left_Leg },
        { PlayerLimb.Right_Leg, PlayerAction.Graft_Right_Leg },
    };

}
