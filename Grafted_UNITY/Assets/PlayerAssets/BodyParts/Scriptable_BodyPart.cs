using UnityEngine;
using UnityEngine.U2D.IK;

using Process = Actions.Process;
using P_Action = Actions.PlayerAction;
using P_Limb = Actions.PlayerLimb;
using Unity.VisualScripting;

public class Scriptable_BodyPart : MonoBehaviour
{
    [SerializeField] private P_Limb slot;
    [SerializeField] private Sprite sprite;

    public Actions.PlayerLimb GetSlot()
    {
        return this.slot;
    }

    public Sprite GetSprite()
    {
        return this.sprite;
    }

    public P_Action GetAction()
    {
        return Actions.ActionTranslator[this.slot];
    }

}
