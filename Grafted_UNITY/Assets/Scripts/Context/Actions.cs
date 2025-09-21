using UnityEngine;

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
}
