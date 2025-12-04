using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public static SceneChanger Instance { get; private set; }

    public struct SceneType {
        public string Name;

        private SceneType(string name) {
            Name = name;
        }

        public static SceneType DeathScreen = new SceneType("DeathScreen");
        public static SceneType VictoryScreen = new SceneType("VictoryScreen");
        public static SceneType MainGame = new SceneType("GraftedLevels");
    }


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(SceneType sceneType) {
        print(sceneType.Name);
        SceneManager.LoadScene(sceneType.Name);
    }
}
