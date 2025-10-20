using UnityEngine;
using System.Collections.Generic;

using Limb = Actions.PlayerLimb;
public class SpriteColorChanger : MonoBehaviour
{
    [SerializeField] private PlayerState P_State;
    [SerializeField] private Dictionary<Limb, BodyPart> Body;


    [Header("Choose the color to apply")]
    [SerializeField] private Color targetColor = Color.red;

    private void Awake()
    {
        if (P_State != null)
            Body = P_State.GetBody();
        else
            Debug.LogWarning("P_State is not assigned!");
    }

    // Call this method to change the color
    public void ChangeColor(SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = targetColor;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not assigned!");
        }
    }

    // Optional: Change color on start
    private void Start()
    {
        foreach(BodyPart bp in Body.Values)
        {
            ChangeColor(bp.GetRenderer());
        }
    }
}
