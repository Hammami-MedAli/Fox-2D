using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class MainMenuSoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
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
            PlayerPrefs.SetFloat("soundVolume", 1);
            loadVolume();
        }
    }
    public void StopMusic()
    {
        source.Stop();
    }
    public void setVolume()
    {
        AudioListener.volume = volumeSlider.value;
        saveVolume();
    }
    public void saveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }
    public void loadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}
