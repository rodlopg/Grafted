using UnityEngine;
using Process = GameState.Process;
using P_Action = PlayerState.PlayerAction;

using P_Limb= PlayerState.PlayerLimb;
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

    public PlayerState.PlayerLimb GetSlot()
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
