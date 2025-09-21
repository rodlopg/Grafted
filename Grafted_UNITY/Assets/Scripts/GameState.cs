using UnityEngine;

public class GameState : MonoBehaviour
{
    public PlayerState PlayerState;
    public PlayerState.PlayerAction LastPlayerAction;

    public enum Process
    {
        START,
        WAIT,
        DONE
    }

}
