using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager S;
    [SerializeField] private AudioClip _clickAc;
    [SerializeField] private AudioClip _connectAc;
    [SerializeField] private AudioClip _correctAc;
    [SerializeField] private AudioClip _incorrectAc;

    [SerializeField] private AudioSource _musicAS;
    [SerializeField] private AudioSource _clickAS;

    private void Start()
    {
        S = this;
        DontDestroyOnLoad(gameObject);

        if (SaveController.S.GetVolumeOn() == 0)
            _clickAS.volume = 0;
        else
            _clickAS.volume = SaveController.S.GetVolume();
        if (SaveController.S.GetMusicOn() == 0)
            _musicAS.volume = 0;
        else
            _musicAS.volume = SaveController.S.GetMusic();
    }

    public void SetVolume(float volume)
    {
        _clickAS.volume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        _musicAS.volume = volume;
    }

    public void PlayClick()
    {
        _clickAS.PlayOneShot(_clickAc);
    }
    public void PlayConnection()
    {
        _clickAS.PlayOneShot(_connectAc);
    }
    public void PlayCorrect()
    {
        _clickAS.PlayOneShot(_correctAc);
    }
    public void PlayIncorrect()
    {
        _clickAS.PlayOneShot(_incorrectAc);
    }
}
