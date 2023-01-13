
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManager;
    public AudioClip[] audioClips;

    public AudioSource musicAS;
    public AudioSource clickAS;


    /// Нумерация звуков:
    /// 0 - кнопка
    /// 1 - соединение клеток
    /// 2 - верно
    /// 3 - неверно
    private void Start()
    {
        _audioManager = this;
        DontDestroyOnLoad(gameObject);

        if (Camera.main.GetComponent<SaveController>().GetVolumeOn() == 0)
            clickAS.volume = 0;
        else
            clickAS.volume = Camera.main.GetComponent<SaveController>().GetVolume();
        if (Camera.main.GetComponent<SaveController>().GetMusicOn() == 0)
            musicAS.volume = 0;
        else
            musicAS.volume = Camera.main.GetComponent<SaveController>().GetMusic(); // ****************************************
    }

    public void SetVolume(float volume)
    {
        clickAS.volume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        musicAS.volume = volume;
    }

    public void PlayClick()
    {
        clickAS.PlayOneShot(audioClips[0]);
    }
    public void PlayConnection()
    {
        clickAS.PlayOneShot(audioClips[1]);
    }
    public void PlayCorrect()
    {
        clickAS.PlayOneShot(audioClips[2]);
    }
    public void PlayIncorrect()
    {
        clickAS.PlayOneShot(audioClips[3]);
    }
}
