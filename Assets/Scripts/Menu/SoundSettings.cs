using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private float musicSliderValue;
    public float MusicSliderValue
    {
        get { return musicSliderValue; }
        set { musicSliderValue = value; musicSlider.value = value; }
    }

    private float soundSliderValue;
    public float SoundSliderValue
    {
        get { return soundSliderValue; }
        set { soundSliderValue = value; soundSlider.value = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadAudioSettings();
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("SoundSliderValue", soundSlider.value);
        PlayerPrefs.Save();

        Debug.Log("Gespeicherte Werte - Musik: " + musicSlider.value + ", Sound: " + soundSlider.value);
    }

    private void LoadAudioSettings()
    {
        musicSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.RemoveAllListeners();

        MusicSliderValue = PlayerPrefs.GetFloat("MusicSliderValue", 1.0f);
        SoundSliderValue = PlayerPrefs.GetFloat("SoundSliderValue", 1.0f);

        musicSlider.onValueChanged.AddListener(OnVolumeChanged);
        soundSlider.onValueChanged.AddListener(OnVolumeChanged);

        Debug.Log("Geladene Werte - Musik: " + MusicSliderValue + ", Sound: " + SoundSliderValue);
    }

    public void OnVolumeChanged(float arg0)
    {
        musicSliderValue = musicSlider.value;
        soundSliderValue = soundSlider.value;

        SaveAudioSettings();
    }
}
