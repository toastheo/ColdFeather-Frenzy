using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private AudioSource[] musicSources;
    private float[] maxMusicVolume;

    private AudioSource[] soundSources;
    private float[] maxSoundVolume;

    [SerializeField] private float maxFlapSoundVolume = 0.5f;
    [SerializeField] private float maxBlopSoundVolume = 0.2f;

    private float flapsoundVolume;
    public float FlapsoundVolume
    {
        get { return flapsoundVolume; }
    }

    private float blopSoundVolume;
    public float BlopSoundVolume
    {
        get { return blopSoundVolume; }
    }

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
        // get all musicSources
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        musicSources = new AudioSource[musicObjects.Length];
        maxMusicVolume = new float[musicObjects.Length];

        for (int i = 0; i < musicObjects.Length; i++)
        {
            musicSources[i] = musicObjects[i].GetComponent<AudioSource>();
            maxMusicVolume[i] = musicSources[i].volume;
        }

        // get all soundSources
        GameObject[] soundObjects = GameObject.FindGameObjectsWithTag("Sound");
        soundSources = new AudioSource[soundObjects.Length];
        maxSoundVolume = new float[soundObjects.Length];

        for (int i = 0; i < soundObjects.Length; i++)
        {
            soundSources[i] = soundObjects[i].GetComponent<AudioSource>();
            maxSoundVolume[i] = soundSources[i].volume;
        }

        // init position of the sliders and sounds
        LoadAudioSettings();
        UpdateAudio();
    }

    private void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MusicSliderValue", musicSlider.value);
        PlayerPrefs.SetFloat("SoundSliderValue", soundSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadAudioSettings()
    {
        musicSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.RemoveAllListeners();

        MusicSliderValue = PlayerPrefs.GetFloat("MusicSliderValue", 1.0f);
        SoundSliderValue = PlayerPrefs.GetFloat("SoundSliderValue", 1.0f);

        musicSlider.onValueChanged.AddListener(OnVolumeChanged);
        soundSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    public void OnVolumeChanged(float arg0)
    {
        musicSliderValue = musicSlider.value;
        soundSliderValue = soundSlider.value;

        UpdateAudio();

        SaveAudioSettings();
    }

    private void UpdateAudio()
    {
        // change music and sound
        for (int i = 0; i < musicSources.Length; i++)
        {
            musicSources[i].volume = musicSlider.value * maxMusicVolume[i];
        }

        for (int i = 0; i < soundSources.Length; i++)
        {
            if (soundSources[i])
                soundSources[i].volume = soundSlider.value * maxSoundVolume[i];
        }

        // get all chickens currently on the screen
        GameObject[] chickenObjects = GameObject.FindGameObjectsWithTag("Chicken");
        AudioSource[] chickenFlapSound = new AudioSource[chickenObjects.Length];
        AudioSource[] chickenBlopSound = new AudioSource[chickenObjects.Length];

        for (int i = 0; i < chickenObjects.Length; i++)
        {
            chickenFlapSound[i] = chickenObjects[i].GetComponent<AudioSource>();
            chickenBlopSound[i] = chickenObjects[i].transform.GetChild(1).GetComponent<AudioSource>();
        }

        for (int i = 0; i < chickenObjects.Length; i++)
        {
            chickenFlapSound[i].volume = soundSlider.value * maxFlapSoundVolume;
            chickenBlopSound[i].volume = soundSlider.value * maxBlopSoundVolume;
        }

        flapsoundVolume = soundSlider.value * maxFlapSoundVolume;
        blopSoundVolume = soundSlider.value * maxBlopSoundVolume;
    }
}
