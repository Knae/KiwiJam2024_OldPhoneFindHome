using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] Button playPause;
    [SerializeField] Slider volume;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Sprite playSprite;
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Image image;

    bool playing = true;

    private void Start()
    {
        volume.onValueChanged.AddListener((float val) =>
        {
            audioSource.volume = val;
        });
        playPause.onClick.AddListener(Switch);
    }

    void Switch()
    {
        playing = !playing;

        if (playing)
            audioSource.Play();
        else
            audioSource.Pause();

        image.sprite = playing ? pauseSprite : playSprite;
    }
}
