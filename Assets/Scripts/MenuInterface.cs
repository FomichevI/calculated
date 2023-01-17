using UnityEngine;
using UnityEngine.UI;

public enum eThemeType { black, blue, white }

public class MenuInterface : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _updatePanel;
    [SerializeField] private GameObject _soundOnImage;
    [SerializeField] private GameObject _soundOffImage;
    [SerializeField] private GameObject _musicOnImage;
    [SerializeField] private GameObject _musicOffImage;
    [SerializeField] private GameObject _russianLangFrame;
    [SerializeField] private GameObject _englishLangFrame;
    [SerializeField] private GameObject _blackThemeFrame;
    [SerializeField] private GameObject _blueThemeFrame;
    [SerializeField] private GameObject _whiteThemeFrame;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _volumeSlider;

    private bool _soundOn = true;
    private bool _musicOn = true;

    private float _soundVolume;
    private float _musicVolume;

    private void Start()
    {
        SetThemeButton(SaveController.S.GetTheme());
        _soundVolume = SaveController.S.GetVolume();
        _musicVolume = SaveController.S.GetMusic();

        if (SaveController.S.GetVolumeOn() == 0)
            SwitchSound();
        else
            _volumeSlider.value = _soundVolume;
        if (SaveController.S.GetMusicOn() == 0)
            SwitchMusic();
        else
            _musicSlider.value = _musicVolume;

        SetLanguageLightning(SaveController.S.GetLanguage());
        LanguageManager.S.RefreshAllTexts();
    }

    public void ShowSettings()
    {
        _settingsPanel.SetActive(true);
        AudioManager.S.PlayClick();
        LanguageManager.S.RefreshAllTexts();
    }
    public void ShowUpdatePanel()
    {
        _updatePanel.SetActive(true);
        LanguageManager.S.RefreshAllTexts();
    }
    public void HideSettings()
    {
        _settingsPanel.SetActive(false);
        AudioManager.S.PlayClick();
    }
    public void HideUpdatePanel()
    {
        _updatePanel.SetActive(false);
        AudioManager.S.PlayClick();
    }

    public void SwitchSound()
    {
        if (_soundOn)
        {
            _soundOnImage.SetActive(false);
            _soundOffImage.SetActive(true);
            _soundOn = false;
            SaveController.S.SetVolumeOn(0);
            AudioManager.S.SetVolume(0);
            _volumeSlider.value = 0;
        }
        else
        {
            _soundOnImage.SetActive(true);
            _soundOffImage.SetActive(false);
            _soundOn = true;
            SaveController.S.SetVolumeOn(1);
            AudioManager.S.SetVolume(_soundVolume);
            _volumeSlider.value = _soundVolume;
        }
    }
    public void SwitchMusic()
    {
        if (_musicOn)
        {
            _musicOnImage.SetActive(false);
            _musicOffImage.SetActive(true);
            _musicOn = false;
            SaveController.S.SetMusicOn(0);
            AudioManager.S.SetMusicVolume(0);
            _musicSlider.value = 0;
        }
        else
        {
            _musicOnImage.SetActive(true);
            _musicOffImage.SetActive(false);
            _musicOn = true;
            SaveController.S.SetMusicOn(1);
            AudioManager.S.SetMusicVolume(_musicVolume);
            _musicSlider.value = _musicVolume;
        }
    }
    public void ChangeTheme(int type)
    {
        SetThemeButton(type);
        SaveController.S.SetTheme(type);
        GetComponent<ThemeChanger>().SetTheme(type);
        AudioManager.S.PlayClick();
    }
    private void SetThemeButton(int theme)
    {
        switch (theme)
        {
            case 1:
                _blackThemeFrame.SetActive(true);
                _blueThemeFrame.SetActive(false);
                _whiteThemeFrame.SetActive(false);
                break;
            case 2:
                _blackThemeFrame.SetActive(false);
                _blueThemeFrame.SetActive(true);
                _whiteThemeFrame.SetActive(false);
                break;
            case 3:
                _blackThemeFrame.SetActive(false);
                _blueThemeFrame.SetActive(false);
                _whiteThemeFrame.SetActive(true);
                break;
        }
    }
    public void SetSoundVolume()
    {
        if (_volumeSlider.value != 0)
        {
            float val = _volumeSlider.value;
            if (_soundOn && val == 0) //если звук включен, но мы убавляем на 0
                SwitchSound();
            else if (!_soundOn && val != 0) //если звук выключен, а мы его включаем
            {
                _soundVolume = val;
                GetComponent<SaveController>().SetVolume(val);
                SwitchSound();
            }
            else //если звук включен и мы его меняем
            {
                _soundVolume = val;
                GetComponent<SaveController>().SetVolume(val);
                AudioManager.S.SetVolume(val);
            }
        }
    }
    public void SetMusicVolume()
    {
        if (_musicSlider.value != 0)
        {
            float val = _musicSlider.value;
            if (_musicOn && val == 0) //если звук включен, но мы убавляем на 0
                SwitchMusic();
            else if (!_musicOn && val != 0) //если звук выключен, а мы его включаем
            {
                _musicVolume = val;
                SaveController.S.SetMusic(val);
                SwitchMusic();
            }
            else //если звук включен и мы его меняем
            {
                _musicVolume = val;
                SaveController.S.SetMusic(val);
                AudioManager.S.SetMusicVolume(val);
            }
        }
    }
    public void SetLanguage(string lang)
    {
        SaveController.S.SetLanguade(lang);
        LanguageManager.S.RefreshAllTexts();
    }
    public void SetLanguageLightning(string lang)
    {
        if (lang == "rus")
        {
            _englishLangFrame.SetActive(false);
            _russianLangFrame.SetActive(true);
        }
        else if (lang == "eng")
        {
            _englishLangFrame.SetActive(true);
            _russianLangFrame.SetActive(false);
        }
        SaveController.S.SetLanguade(lang);
    }
}
