using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Settings")]
    public AudioSource bgMusic;
    public AudioSource clickSFX;

    [Header("UI Reference")]
    public Image btnIcon;
    public Sprite onIcon;
    public Sprite offIcon;

    private bool isMuted = false;

    // --- Singleton Setup ---
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Sync music with saved settings
        if (bgMusic != null)
        {
            bgMusic.mute = isMuted;
        }

        // Find toggle button icon at start
        if (btnIcon == null)
        {
            GameObject foundObj = GameObject.Find("MusicToggleButton");
            if (foundObj != null)
            {
                btnIcon = foundObj.GetComponent<Image>();
                btnIcon.sprite = isMuted ? offIcon : onIcon;
            }
        }
    }

    // --- Scene Switching Logic ---
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Music only plays in specific scenes
        if (scene.name == "MainMenu" || scene.name == "MySimulation" || scene.name == "Tutorial" || scene.name == "Credit")
        {
            if (bgMusic != null && !bgMusic.isPlaying)
            {
                bgMusic.Play();
                bgMusic.mute = isMuted;
            }
        }
        else
        {
            if (bgMusic != null) bgMusic.Stop();
        }
    }

    // --- Button Management ---
    void Update()
    {
        // Re-find buttons if they are lost after scene change
        if (btnIcon == null)
        {
            FindAndFixButton();
        }
    }

    void FindAndFixButton()
    {
        GameObject foundObj = GameObject.Find("MusicToggleButton");

        if (foundObj != null)
        {
            btnIcon = foundObj.GetComponent<Image>();
            btnIcon.sprite = isMuted ? offIcon : onIcon;

            Button btn = foundObj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(ToggleMusic);
            btn.onClick.AddListener(PlayClickSound);
        }

        // Apply sounds to menu buttons
        AddSoundToButton("PlayButton");
        AddSoundToButton("TutorialButton");
        AddSoundToButton("CreditButton");
        AddSoundToButton("ExitButton");
    }

    void AddSoundToButton(string btnName)
    {
        GameObject btnObj = GameObject.Find(btnName);
        if (btnObj != null)
        {
            Button btn = btnObj.GetComponent<Button>();
            btn.onClick.RemoveListener(PlayClickSound);
            btn.onClick.AddListener(PlayClickSound);
        }
    }

    // --- Audio Functions ---
    public void ToggleMusic()
    {
        isMuted = !isMuted;
        bgMusic.mute = isMuted;

        if (btnIcon != null)
        {
            btnIcon.sprite = isMuted ? offIcon : onIcon;
        }

        SaveSettings();
    }

    public void PlayClickSound()
    {
        if (clickSFX != null)
        {
            clickSFX.Play();
        }
    }

    // --- Save System ---
    void SaveSettings()
    {
        PlayerPrefs.SetInt("MutedState", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        isMuted = PlayerPrefs.GetInt("MutedState", 0) == 1;
    }

    // --- UI Effects ---
    public void OnHover() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1f); }
    public void OnExit() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1f, 1f, 1f); }
    public void OnClickDown() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(0.9f, 0.9f, 1f); }
    public void OnClickUp() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1f); }
}