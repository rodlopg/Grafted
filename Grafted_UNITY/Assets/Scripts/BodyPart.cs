using UnityEngine;

public class BodyPart {
    private PlayerState.PlayerLimb slot;
    private GameObject LimbObject;
    private SpriteRenderer Renderer;

    public BodyPart(PlayerState.PlayerLimb slot, GameObject LimbObject)
    {
        this.slot = slot;
        this.LimbObject = LimbObject;
        this.Renderer = LimbObject.GetComponent<SpriteRenderer>();
    }

    public bool Graft(Sprite newSprite)
    {
        this.Renderer.sprite = newSprite;
        return true;
    }
}
