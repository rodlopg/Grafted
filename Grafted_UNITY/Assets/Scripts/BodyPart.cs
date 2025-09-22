using UnityEngine;
using Process = Actions.Process;
using P_Action = Actions.PlayerAction;
using P_Limb = Actions.PlayerLimb;

public class BodyPart : ScriptableObject
{
    // Limb slot this body part belongs to (e.g., head, torso, arm, etc.)
    [SerializeField] private P_Limb slot;

    // The actual GameObject in the scene representing this limb
    private GameObject LimbObject;
    // SpriteRenderer attached to the limb GameObject
    private SpriteRenderer Renderer;

    // Constructor: creates a new BodyPart and assigns its object/renderer
    public BodyPart(P_Limb slot, GameObject LimbObject)
    {
        this.slot = slot;
        this.LimbObject = LimbObject;
        this.Renderer = LimbObject.GetComponent<SpriteRenderer>();
    }

    // Replaces the sprite of this body part
    public Process Graft(Sprite newSprite)
    {
        this.Renderer.sprite = newSprite;
        return Process.DONE;
    }

    // Returns the slot type (which limb this is)
    public Actions.PlayerLimb GetSlot()
    {
        return this.slot;
    }

    // Returns the current sprite of the body part
    public Sprite GetSprite()
    {
        return this.Renderer.sprite;
    }

    // Returns the graft action associated with this limb type
    public P_Action GetAction()
    {
        return Actions.ActionTranslator[this.slot];
    }
}
