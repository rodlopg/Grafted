using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using E_Action = Actions.EnemyAction;
using Limb = Actions.PlayerLimb;
using P_Action = Actions.PlayerAction;
using Process = Actions.Process;

public class GameProvider : MonoBehaviour
{
    // Reference to the global GameState (ScriptableObject)
    [SerializeField] private GameState G_State;
    // Reference to the PlayerState (MonoBehaviour managing current body parts)
    [SerializeField] private PlayerState P_State;
    [SerializeField] private Image E_Key;
    private Scriptable_BodyPart lastLimb = null;
    [SerializeField] private Color targetColor = Color.red;

    public static EventHandler<BodyPartEventArgs> onBodyPartDetection;

    public class BodyPartEventArgs : EventArgs
    {
        public Limb Slot { get; private set; }

        public BodyPartEventArgs(Limb slot)
        {
            Slot = slot;
        }
    }

    public void Update()
    {
        Scriptable_BodyPart limb = P_State.CheckNearbyBodyParts();
        onBodyPartDetection?.Invoke(this, new BodyPartEventArgs(limb.GetSlot()));
        if (limb != null)
        {
            lastLimb = limb;
            Show_E_Key();
            P_State.Show(limb.GetSlot());
            Debug.Log("Changing color of -> " + P_State.GetBody()[limb.GetSlot()].GetSlot());
        }
        else
        {
            P_State.GetBody()[lastLimb.GetSlot()].Stop_Show();
            Hide_E_Key();
        }
    }
    // Handles grafting a new limb into the player
    public Process Provide_Graft()
    {
        // Try to graft the new limb onto the player
        Scriptable_BodyPart limb = P_State.CheckNearbyBodyParts();
        if (P_State.GraftLimb(limb) == Process.DONE)
        {
            // If graft succeeded, update the GameState with the last action performed
            GameState.SetLastPlayerAction(limb.GetAction());
        }

        return Process.DONE; // Always returns DONE for now
    }

    public static Process Animate(Animator animator, P_Action playerAction=P_Action.NULL, E_Action enemyAction=E_Action.NULL)
    {
        List<string> conditions;

        if (playerAction == P_Action.NULL)
        {
            E_Action action = enemyAction;
            conditions = Actions.EnemyAnimations[action];
            
        }
        else
        {
            P_Action action = playerAction;
            conditions = Actions.PlayerAnimations[action];
        }

        if (conditions != null)
        {
            foreach (string c in conditions)
            {
                animator.SetTrigger(c);
            }
        }

        return Process.DONE;
    }
    public static Process Animate(Animator animator, E_Action enemyAction)
    {
        return Animate(animator, P_Action.NULL, enemyAction);
    }

    public Process Show_E_Key()
    {
        E_Key.gameObject.SetActive(true);

        return Process.DONE;
    }

    public Process Hide_E_Key()
    {
        E_Key.gameObject.SetActive(false);

        return Process.DONE;
    }
}
