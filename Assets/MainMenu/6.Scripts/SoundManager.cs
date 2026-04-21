using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // --- SETTINGS AND VARIABLES ---
    public static SoundManager instance;

    [Header("Audio Settings")]
    public AudioSource bgMusic;
    public AudioSource clickSFX;

    [Header("UI Reference")]
    public Image btnIcon;
    public Sprite onIcon;
    public Sprite offIcon;

    private bool isMuted = false;

    // --- INITIALIZATION ---
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
        // Sync music volume with saved settings at start
        if (bgMusic != null)
        {
            bgMusic.mute = isMuted;
        }

        // Find the toggle button icon when game starts
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

    // --- SCENE MANAGEMENT ---
    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only play music in Menu, Tutorial, and Credit scenes
        if (scene.name == "MainMenu_Scene" || scene.name == "Tutorial" || scene.name == "Credit")
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

    // --- BUTTON SYSTEM ---
    void Update()
    {
        // If scene changed and we lost the button reference, find it again
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

        // Link sounds to all main menu buttons
        AddSoundToButton("Start");
        AddSoundToButton("Tutorial");
        AddSoundToButton("Credit");
        AddSoundToButton("Exit");
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

    // --- CORE AUDIO LOGIC ---
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

    // --- SAVE AND LOAD ---
    void SaveSettings()
    {
        PlayerPrefs.SetInt("MutedState", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        isMuted = PlayerPrefs.GetInt("MutedState", 0) == 1;
    }

    // --- UI ANIMATIONS (HOVER) ---
    public void OnHover() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1f); }
    public void OnExit() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1f, 1f, 1f); }
    public void OnClickDown() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(0.9f, 0.9f, 1f); }
    public void OnClickUp() { if (btnIcon != null) btnIcon.transform.localScale = new Vector3(1.2f, 1.2f, 1f); }
}