using UnityEngine;

public class EndScreenUIActions : MonoBehaviour
{
   public void tryAgain() {
        print(SceneChanger.SceneType.MainGame.Name);
        SceneChanger.Instance.ChangeScene(SceneChanger.SceneType.MainGame);
   }

    public void exitGame() {
        Application.Quit();
    }
}
