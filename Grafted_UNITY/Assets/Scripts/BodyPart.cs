using UnityEngine;
using Process = Actions.Process;
using P_Action = Actions.PlayerAction;
using P_Limb= Actions.PlayerLimb;

public class BodyPart {
    private P_Limb slot;
    private P_Action action;
    private GameObject LimbObject;
    private SpriteRenderer Renderer;

    public BodyPart(P_Limb slot, P_Action action, GameObject LimbObject)
    {
        this.slot = slot;
        this.action = action;
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
        return this.action;
    }
}
