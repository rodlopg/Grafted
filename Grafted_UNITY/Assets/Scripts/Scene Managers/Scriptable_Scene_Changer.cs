using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable_Scene_Changer", menuName = "Scriptable Objects/Scriptable_Scene_Changer")]
public class Scriptable_Scene_Changer : ScriptableObject
{
    // Method to load a scene by its name
    public static void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to load a scene by its build index
    public static void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
