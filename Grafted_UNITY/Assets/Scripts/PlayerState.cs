using UnityEngine;
using System.Collections.Generic;
using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject Head_Object;
    [SerializeField] private GameObject Torso_Object;
    [SerializeField] private GameObject Left_Arm_Object;
    [SerializeField] private GameObject Right_Arm_Object;
    [SerializeField] private GameObject Left_Leg_Object;
    [SerializeField] private GameObject Right_Leg_Object;

    private Dictionary<Limb, BodyPart> Body;

    private void Awake()
    {
        Body = new Dictionary<Limb, BodyPart>{ 
            { Limb.Head, new BodyPart(Limb.Head, P_Action.Graft_Head, Head_Object) },
            { Limb.Torso, new BodyPart(Limb.Torso, P_Action.Graft_Torso, Torso_Object) },
            { Limb.Left_Arm, new BodyPart(Limb.Left_Arm, P_Action.Graft_Left_Arm, Left_Arm_Object) },
            { Limb.Right_Arm, new BodyPart(Limb.Right_Arm, P_Action.Graft_Right_Arm, Right_Arm_Object) },
            { Limb.Left_Leg, new BodyPart(Limb.Left_Leg, P_Action.Graft_Left_Leg, Left_Leg_Object) },
            { Limb.Right_Leg, new BodyPart(Limb.Right_Leg, P_Action.Graft_Right_Leg, Right_Leg_Object) }
        };
    }

    public Process GraftLimb(Scriptable_BodyPart newLimb)
    {
        Body[newLimb.GetSlot()].Graft(newLimb.GetSprite());
        return Process.DONE;
    }

}
