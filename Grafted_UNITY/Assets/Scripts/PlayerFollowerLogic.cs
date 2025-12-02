using UnityEngine;

public class PlayerFollowerLogic : MonoBehaviour {
    public Transform target;
    public GameObject snowball;

    private float snowballTimer = 2f;

    void Update() {
        transform.LookAt(target);
        snowballTimer -= Time.deltaTime;

        if (snowballTimer < 0f) {
            Instantiate(snowball, new Vector3(9.03f, target.localPosition.y - 1, 0), Quaternion.identity);
            snowballTimer = 2f;
        }
    }
}