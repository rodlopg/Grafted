using UnityEngine;

public class GraftTester : MonoBehaviour
{
    [Header("Assign the GameProvider")]
    [SerializeField] private GameProvider gameProvider;

    [Header("Prefabs with Body Parts")]
    [SerializeField] private GameObject[] bodyPartPrefabs;

    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bodyPartPrefabs.Length == 0 || gameProvider == null) return;

            // Get the Scriptable_BodyPart from the current prefab
            Scriptable_BodyPart part = bodyPartPrefabs[currentIndex].GetComponent<Scriptable_BodyPart>();
            if (part != null)
            {
                gameProvider.Provide_Graft(part);
            }

            // Move to the next prefab in the list
            currentIndex = (currentIndex + 1) % bodyPartPrefabs.Length;
        }
    }
}
