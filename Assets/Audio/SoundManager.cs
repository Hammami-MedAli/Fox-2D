using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set;}
    private AudioSource source;
    public Slider volumeSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            loadVolume();
        }
        else
        {
            PlayerPrefs.SetFloat("soundVolume",1);
            loadVolume();   
        }
    }
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
    public void StopMusic()
    {
        source.Stop();    }
    public void setVolume()
    {
        AudioListener.volume = volumeSlider.value;
        saveVolume();
    }
    public void saveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume",volumeSlider.value)  ;
    }
    public void loadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}
