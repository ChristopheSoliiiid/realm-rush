using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonHandler : MonoBehaviour
{
    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;
    [SerializeField] GameObject soundToToggle;
    [SerializeField] float defaultVolume = 0.5f;

    Image image;
    Button button;
    bool isSoundOn = true;
    AudioSource audioSource;
    float previousVolume;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = soundOnSprite;

        audioSource = soundToToggle != null ? soundToToggle.GetComponent<AudioSource>() : null;
        previousVolume = audioSource != null ? audioSource.volume : defaultVolume;

        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleSoundAndSprite);
    }

    void ToggleSoundAndSprite()
    {
        isSoundOn = !isSoundOn;

        ToggleSprite();
        ToggleSound();
    }

    void ToggleSprite()
    {
        image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }

    void ToggleSound()
    {
        if (audioSource != null) {
            audioSource.volume = isSoundOn ? previousVolume : 0f;
        } else {
            AudioListener.volume = isSoundOn ? 1f : 0f;
        }
    }
}
