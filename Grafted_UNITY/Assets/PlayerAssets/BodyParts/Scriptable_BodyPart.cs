using UnityEngine;
using UnityEngine.U2D.IK;

using Process = Actions.Process;          // Alias for Actions.Process enum
using P_Action = Actions.PlayerAction;    // Alias for Actions.PlayerAction enum
using P_Limb = Actions.PlayerLimb;        // Alias for Actions.PlayerLimb enum
using Unity.VisualScripting;
using UnityEditor;

public class Scriptable_BodyPart : MonoBehaviour
{
    // The limb slot this body part corresponds to (Head, Torso, Left_Arm, etc.)
    [SerializeField] private P_Limb slot;

    // The sprite assigned to this body part
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject prefab;


    private void Awake()
    {
        // Try to get a SpriteRenderer component on the same GameObject
        var renderer = gameObject.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            // If found, assign the serialized sprite to it
            renderer.sprite = this.sprite;
        }
        else
        {
            // If no SpriteRenderer exists, warn the developer
            Debug.LogWarning("No SpriteRenderer found on this GameObject!");
        }
    }

    // Returns the limb slot type (which part of the body this object is)
    public Actions.PlayerLimb GetSlot()
    {
        return this.slot;
    }

    // Returns the sprite assigned to this body part
    public Sprite GetSprite()
    {
        return this.sprite;
    }

    // Returns the PlayerAction associated with this limb
    public P_Action GetAction()
    {
        return Actions.ActionTranslator[this.slot];
    }

    public GameObject GetPrefab()
    {
        return this.prefab;
    }
}
