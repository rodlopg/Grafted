using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        FindCameraAudioSource();
    }

    private void Start() {
        FindCameraAudioSource();
    }

    private void FindCameraAudioSource() {
        Camera cam = Camera.main;

        if (cam == null) {
            return;
        }

        mainCameraAudioSource = cam.GetComponent<AudioSource>();
    }

    public void changeMusic(AudioClip newClip) {
        if (newClip == null || mainCameraAudioSource == null) return;

        mainCameraAudioSource.clip = newClip;
        mainCameraAudioSource.Play();
    }
}
