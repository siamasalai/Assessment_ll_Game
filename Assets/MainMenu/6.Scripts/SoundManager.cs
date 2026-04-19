using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public Image btnIcon;
    public Sprite onIcon;
    public Sprite offIcon;

    private bool isMuted = false;

    // Set how big the icon gets in the Inspector
    public float hoverSize = 1.2f;
    public float clickSize = 0.9f;

    void Start()
    {
        // Sync the logic with the AudioSource settings
        isMuted = bgMusic.mute;

        if (isMuted)
        {
            btnIcon.sprite = offIcon;
        }
        else
        {
            btnIcon.sprite = onIcon;
        }
    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        bgMusic.mute = isMuted;

        if (isMuted)
        {
            btnIcon.sprite = offIcon;
        }
        else
        {
            btnIcon.sprite = onIcon;
        }
    }

    // --- Hover and Click Effects ---

    public void OnHover()
    {
        btnIcon.transform.localScale = new Vector3(hoverSize, hoverSize, 1f);
    }

    public void OnExit()
    {
        // Back to normal size
        btnIcon.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnClickDown()
    {
        // Shrink when pressed
        btnIcon.transform.localScale = new Vector3(clickSize, clickSize, 1f);
    }

    public void OnClickUp()
    {
        // Reset to hover size when finger/mouse is lifted
        btnIcon.transform.localScale = new Vector3(hoverSize, hoverSize, 1f);
    }
}
