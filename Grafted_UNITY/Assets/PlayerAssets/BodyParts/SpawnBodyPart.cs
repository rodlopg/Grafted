using System.Collections.Generic;
using UnityEngine;

public class SpawnBodyPart : MonoBehaviour
{
    [Header("Assign your Scriptable_BodyPart assets here")]
    [SerializeField] private List<Scriptable_BodyPart> bodyPartAssets = new List<Scriptable_BodyPart>();

    // Static list to track available parts across the game
    private static List<Scriptable_BodyPart> availableParts;

    private void Awake()
    {
        // Copy the serialized list into the static one
        availableParts = new List<Scriptable_BodyPart>(bodyPartAssets);

        Debug.Log($"✅ Loaded {availableParts.Count} body parts from Inspector.");
    }

    /// <summary>
    /// Spawns a random Scriptable_BodyPart prefab in the world at a given position.
    /// </summary>
    public static GameObject SpawnRandomBodyPart(Vector3 position)
    {
        if (availableParts == null || availableParts.Count == 0)
        {
            Debug.LogWarning("⚠️ No available body parts to spawn!");
            return null;
        }

        // Pick a random index
        int randomIndex = Random.Range(0, availableParts.Count);
        Scriptable_BodyPart chosenPart = availableParts[randomIndex];

        // Check prefab assignment
        if (chosenPart == null || chosenPart.GetPrefab() == null)
        {
            Debug.LogWarning($"⚠️ Scriptable_BodyPart '{chosenPart?.name}' has no prefab assigned.");
            return null;
        }

        // Instantiate prefab at the given position
        GameObject newPart = Instantiate(chosenPart.GetPrefab(), position, Quaternion.identity);
        Debug.Log($"🧠 Spawned '{newPart.name}' at {position}");

        // Remove it from the list so it won’t be spawned again
        availableParts.RemoveAt(randomIndex);

        return newPart;
    }

    public static List<Scriptable_BodyPart> GetScriptable_BodyParts()
    {
        return availableParts;
    }
}
