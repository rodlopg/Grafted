using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using static Actions;
using static GameProvider;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;
using Process = Actions.Process;
using Random = UnityEngine.Random;

public class PlayerState : MonoBehaviour
{
    // References to the player's body parts in the scene
    [SerializeField] private GameObject Head_Object;
    [SerializeField] private GameObject Torso_Object;
    [SerializeField] private GameObject Left_Arm_Object;
    [SerializeField] private GameObject Right_Arm_Object;
    [SerializeField] private GameObject Left_Leg_Object;
    [SerializeField] private GameObject Right_Leg_Object;
    [SerializeField] private PlayerInteractions P_Interactions;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D circleCollider;
    private Scriptable_BodyPart lastDetectedPart;




    [Header("Body Part Detection")]
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask bodyPartLayer;

    // Dictionary mapping limb types to BodyPart objects
    private Dictionary<Limb, BodyPart> Body;

    // Maps each limb to the corresponding graft action
    public Dictionary<PlayerLimb, AnimationClip> AnimationTranslator;



    public static EventHandler<BodyPartEventArgs> onBodyPartDetection;

    public class BodyPartEventArgs : EventArgs
    {
        public Limb Slot { get; private set; }

        public BodyPartEventArgs(Limb slot)
        {
            Slot = slot;
        }
    }

    private void Awake()
    {
        circleCollider.radius = detectionRadius;
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
        Sprite copy = Body[newLimb.GetSlot()].GetSprite();

        Body[newLimb.GetSlot()].Graft(newLimb.GetSprite());
        P_Interactions.changeSpeed(Random.Range(0.01f, 1f));
        P_Interactions.changeStrength(Random.Range(0.01f, 1f));

        newLimb.SetSprite(copy);
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

    /*
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
    */

    public Scriptable_BodyPart CheckNearbyBodyParts()
    {
        if (lastDetectedPart != null)
        {
            return lastDetectedPart;
        }

        Debug.Log("No body part found nearby (trigger).");
        return null;
    }


    public Process Show(Limb limb) {
        if (limb != this.lastDetectedPart.GetSlot()) { 

        }
        this.animator.Play(Actions.AnimationTranslator[limb]);
        return Process.DONE;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var part = collision.GetComponent<Scriptable_BodyPart>();
        if (part != null)
        {
            lastDetectedPart = part;
            onBodyPartDetection?.Invoke(this, new BodyPartEventArgs(part.GetSlot()));
            Debug.Log($"Detected body part: {part.GetSlot()}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var part = collision.GetComponent<Scriptable_BodyPart>();
        if (part != null && part == lastDetectedPart)
        {
            Debug.Log("Body part left detection area");
            lastDetectedPart = null;
            onBodyPartDetection?.Invoke(this, new BodyPartEventArgs(Limb.NULL));
        }
    }


}
