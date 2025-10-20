using UnityEngine;
using System.Collections.Generic;

public class Actions : MonoBehaviour
{
    // Represents a simple process lifecycle (used for grafting and state changes)
    public enum Process
    {
        START,
        WAIT,
        DONE
    }

    // Different possible player limbs
    public enum PlayerLimb
    {
        Head,
        Torso,
        Left_Arm,
        Right_Arm,
        Left_Leg,
        Right_Leg,
    }

    // Different possible player actions
    public enum PlayerAction
    {
        Idle,
        Dash,
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

    // Maps each limb to the corresponding graft action
    public static Dictionary<PlayerLimb, PlayerAction> ActionTranslator = new Dictionary<PlayerLimb, PlayerAction>
    {
        { PlayerLimb.Head, PlayerAction.Graft_Head },
        { PlayerLimb.Torso, PlayerAction.Graft_Torso },
        { PlayerLimb.Left_Arm, PlayerAction.Graft_Left_Arm },
        { PlayerLimb.Right_Arm, PlayerAction.Graft_Right_Arm },
        { PlayerLimb.Left_Leg, PlayerAction.Graft_Left_Leg },
        { PlayerLimb.Right_Leg, PlayerAction.Graft_Right_Leg },
    };

    public static Dictionary<PlayerAction, List<string>> AnimationTranslator = new Dictionary<PlayerAction, List<string>>
    {
        { PlayerAction.Idle, new List<string> { "isIdle" } },
        { PlayerAction.Dash, new List<string> { "isDash" } },
        { PlayerAction.Right_Walk, new List<string> { "isWalking", "isMovingRight" } },
        { PlayerAction.Left_Walk, new List<string> { "isWalking", "isMovingLeft" } },
        { PlayerAction.Right_Attack, new List<string> { "isAttackingRight" } },
        { PlayerAction.Left_Attack, new List<string> { "isAttackingLeft" } },
        { PlayerAction.Jump, new List<string> { "isJumping" } },
        { PlayerAction.Graft_Head, new List<string> { "isGraftingHead" } },
        { PlayerAction.Graft_Torso, new List<string> { "isGraftingTorso" } },
        { PlayerAction.Graft_Left_Arm, new List<string> { "isGraftingLeftArm" } },
        { PlayerAction.Graft_Right_Arm, new List<string> { "isGraftingRightArm" } },
        { PlayerAction.Graft_Left_Leg, new List<string> { "isGraftingLeftLeg" } },
        { PlayerAction.Graft_Right_Leg, new List<string> { "isGraftingRightLeg" } },
    };
}
