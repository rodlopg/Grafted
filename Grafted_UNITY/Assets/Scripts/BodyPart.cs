using UnityEngine;
using Process = Actions.Process;
using P_Action = Actions.PlayerAction;
using P_Limb= Actions.PlayerLimb;

public class BodyPart : ScriptableObject {
    [SerializeField] private P_Limb slot;
    private GameObject LimbObject;
    private SpriteRenderer Renderer;

    public BodyPart(P_Limb slot, GameObject LimbObject)
    {
        this.slot = slot;
        this.LimbObject = LimbObject;
        this.Renderer = LimbObject.GetComponent<SpriteRenderer>();
    }

    public Process Graft(Sprite newSprite)
    {
        this.Renderer.sprite = newSprite;
        return Process.DONE;
    }

    public Actions.PlayerLimb GetSlot()
    {
        return this.slot;
    }

    public Sprite GetSprite()
    {
        return this.Renderer.sprite;
    }
    public P_Action GetAction()
    {
        return Actions.ActionTranslator[this.slot];
    }
}
