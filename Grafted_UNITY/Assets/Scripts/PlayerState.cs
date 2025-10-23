using UnityEngine;
using System.Collections.Generic;
using Process = Actions.Process;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;

public class PlayerState : MonoBehaviour
{
    // References to the player's body parts in the scene
    [SerializeField] private GameObject Head_Object;
    [SerializeField] private GameObject Torso_Object;
    [SerializeField] private GameObject Left_Arm_Object;
    [SerializeField] private GameObject Right_Arm_Object;
    [SerializeField] private GameObject Left_Leg_Object;
    [SerializeField] private GameObject Right_Leg_Object;

    [Header("Body Part Detection")]
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask bodyPartLayer;

    // Dictionary mapping limb types to BodyPart objects
    private Dictionary<Limb, BodyPart> Body;
    


    private void Awake()
    {
        // Initialize the dictionary with body part instances
        Body = new Dictionary<Limb, BodyPart>{ 
            { Limb.Head, new BodyPart(Limb.Head, Head_Object) },
            { Limb.Torso, new BodyPart(Limb.Torso, Torso_Object) },
            { Limb.Left_Arm, new BodyPart(Limb.Left_Arm, Left_Arm_Object) },
            { Limb.Right_Arm, new BodyPart(Limb.Right_Arm, Right_Arm_Object) },
            { Limb.Left_Leg, new BodyPart(Limb.Left_Leg, Left_Leg_Object) },
            { Limb.Right_Leg, new BodyPart(Limb.Right_Leg, Right_Leg_Object) }
        };
    }

    // Grafts a new limb by swapping its sprite
    public Process GraftLimb(Scriptable_BodyPart newLimb)
    {
        Body[newLimb.GetSlot()].Graft(newLimb.GetSprite());
        return Process.DONE;
    }

    public Dictionary<Limb, BodyPart> GetBody()
    {
        return this.Body;
    }

    public static Process Animate(Animator animator, P_Action action)
    {
        List<string> conditions = Actions.PlayerAnimations[action];
        if (conditions != null)
        {
            foreach (string c in conditions)
            {
                animator.SetTrigger(c);
            }
        }

        /*
        foreach(P_Action a in Actions.AnimationTranslator.Keys)
        {
            
            if(a == action)
            {
                foreach(string c in conditions)
                {
                    animator.SetTrigger(c);
                }
            }
            else
            {
                foreach (string c in conditions)
                {
                    animator.SetBool(c, false);
                }
            }
            
            
        }
        */
        return Process.DONE;
    }

    public Scriptable_BodyPart CheckNearbyBodyParts()
    {
        Collider2D[] nearby = Physics2D.OverlapCircleAll(transform.position, detectionRadius, bodyPartLayer);

        foreach (Collider2D col in nearby)
        {
            // Assuming your prefab has a component linking to its ScriptableObject
            Scriptable_BodyPart partInstance = col.GetComponent<Scriptable_BodyPart>();
            if (partInstance != null)
            {
                
                Debug.Log($"Found body part: {partInstance.GetSlot()}");
                return partInstance; // Return the associated Scriptable_BodyPart
            }
        }

        Debug.Log("No body part found nearby.");
        return null;
    }

}
