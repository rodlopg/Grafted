using UnityEngine;
using System.Collections.Generic;
using Process = GameState.Process;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject Head_Object;
    [SerializeField] private GameObject Torso_Object;
    [SerializeField] private GameObject Left_Arm_Object;
    [SerializeField] private GameObject Right_Arm_Object;
    [SerializeField] private GameObject Left_Leg_Object;
    [SerializeField] private GameObject Right_Leg_Object;

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

    private Dictionary<PlayerLimb, BodyPart> Body;

    private void Awake()
    {
        Body = new Dictionary<PlayerLimb, BodyPart>{ 
            { PlayerLimb.Head, new BodyPart(PlayerLimb.Head, PlayerAction.Graft_Head, Head_Object) },
            { PlayerLimb.Torso, new BodyPart(PlayerLimb.Torso, PlayerAction.Graft_Torso, Torso_Object) },
            { PlayerLimb.Left_Arm, new BodyPart(PlayerLimb.Left_Arm, PlayerAction.Graft_Left_Arm, Left_Arm_Object) },
            { PlayerLimb.Right_Arm, new BodyPart(PlayerLimb.Right_Arm, PlayerAction.Graft_Right_Arm, Right_Arm_Object) },
            { PlayerLimb.Left_Leg, new BodyPart(PlayerLimb.Left_Leg, PlayerAction.Graft_Left_Leg, Left_Leg_Object) },
            { PlayerLimb.Right_Leg, new BodyPart(PlayerLimb.Right_Leg, PlayerAction.Graft_Right_Leg, Right_Leg_Object) }
        };
    }

    public Process GraftLimb(BodyPart limb)
    {
        Body[limb.GetSlot()].Graft(limb.GetSprite());
        return Process.DONE;
    }

}
