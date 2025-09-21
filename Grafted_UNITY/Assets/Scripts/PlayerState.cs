using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject Head_Object;
    [SerializeField] private GameObject Torso_Object;
    [SerializeField] private GameObject Left_Arm_Object;
    [SerializeField] private GameObject Right_Arm_Object;
    [SerializeField] private GameObject Left_Leg_Object;
    [SerializeField] private GameObject Right_Leg_Object;

    private BodyPart Head;
    private BodyPart Torso;
    private BodyPart Left_Arm;
    private BodyPart Right_Arm;
    private BodyPart Left_Leg;
    private BodyPart Right_Leg;

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

    private void Awake()
    {
        Head = new BodyPart(PlayerLimb.Head, Head_Object);
        Torso = new BodyPart(PlayerLimb.Torso, Torso_Object);
        Left_Arm = new BodyPart(PlayerLimb.Left_Arm, Left_Arm_Object);
        Right_Arm = new BodyPart(PlayerLimb.Right_Arm, Right_Arm_Object);
        Left_Leg = new BodyPart(PlayerLimb.Left_Leg, Left_Leg_Object);
        Right_Leg = new BodyPart(PlayerLimb.Right_Leg, Right_Leg_Object);
    }
}
