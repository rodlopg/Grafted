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
        NULL
    }

    // Different possible player actions
    public enum PlayerAction
    {
        Idle,
        Take_Damage,
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
        Show_Head,
        Show_Torso,
        Show_Left_Arm,
        Show_Right_Arm,
        Show_Left_Leg,
        Show_Right_Leg,
        NULL
    }

    public enum EnemyAction
    {
        Idle,
        Take_Damage,
        Die,
        Dash,
        Right_Walk,
        Left_Walk,
        Right_Attack,
        Left_Attack,
        Jump,
        NULL
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

    // Maps each limb to the corresponding graft action
    public static Dictionary<PlayerLimb, string> AnimationTranslator = new Dictionary<PlayerLimb, string>
    {
        { PlayerLimb.Head, "ShowHead" },
        { PlayerLimb.Torso, "ShowTorso" },
        { PlayerLimb.Left_Arm, "ShowLeftArm" },
        { PlayerLimb.Right_Arm, "ShowRightArm" },
        { PlayerLimb.Left_Leg, "ShowLeftLeg" },
        { PlayerLimb.Right_Leg, "ShowRightLeg" },
    };

    public static Dictionary<PlayerAction, List<string>> PlayerAnimations= new Dictionary<PlayerAction, List<string>>
    {
        { PlayerAction.Idle, new List<string> { "isIdle" } },
        { PlayerAction.Take_Damage, new List<string> { "isHit" } },
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

    public static Dictionary<EnemyAction, List<string>> EnemyAnimations = new Dictionary<EnemyAction, List<string>>
    {
        { EnemyAction.Idle, new List<string> { "isIdle" } },
        { EnemyAction.Take_Damage, new List<string> { "isHit" } },
        { EnemyAction.Die, new List<string> { "isDying" } },
        { EnemyAction.Dash, new List<string> { "isDashing" } },
        { EnemyAction.Right_Walk, new List<string> { "isWalking", "isMovingRight" } },
        { EnemyAction.Left_Walk, new List<string> { "isWalking", "isMovingLeft" } },
        { EnemyAction.Right_Attack, new List<string> { "isAttackingRight" } },
        { EnemyAction.Left_Attack, new List<string> { "isAttackingLeft" } },
        { EnemyAction.Jump, new List<string> { "isJumping" } }
    };

}
