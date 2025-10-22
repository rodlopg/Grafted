using UnityEngine;
using UnityEngine.InputSystem;

public class GameReducer : MonoBehaviour
{
   /*
    
    public void Graft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (SpawnBodyPart.GetScriptable_BodyParts().Count== 0 || SpawnBodyPart.GetScriptable_BodyParts() == null) return;

            // Get the Scriptable_BodyPart from the current prefab
            //Scriptable_BodyPart part = bodyPartPrefabs[currentIndex].GetComponent<Scriptable_BodyPart>();
            Scriptable_BodyPart part = SpawnBodyPart.SpawnRandomBodyPart();
            if (part != null)
            {
                gameProvider.Provide_Graft(part);
            }

            // Move to the next prefab in the list
            currentIndex = (currentIndex + 1) % bodyPartPrefabs.Length;
        }
    }
    */
}
