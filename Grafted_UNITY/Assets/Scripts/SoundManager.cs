using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource mainCameraAudioSource;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void changeMusic(AudioClip newClip) {
        if (newClip == null) return;

        mainCameraAudioSource.clip = newClip;
        mainCameraAudioSource.Play();
    }
}