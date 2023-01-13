using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInterface : MonoBehaviour
{
    [SerializeField] private GameObject _rulesPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private GameObject _waitPanel;

    private bool _panelsIsActive; public bool PanelsIsActive { get { return _panelsIsActive; } }

    private void Start()
    {
        LanguageManager.S.RefreshAllTexts();
    }

    public void ShowRules()
    {
        _rulesPanel.SetActive(true);
        GetComponent<GameManager>().isPlaying = false;
        AudioManager._audioManager.PlayClick();
        _panelsIsActive = true;
        LanguageManager.S.RefreshAllTexts();
    }

    public void HideAllpanels()
    {
        _rulesPanel.SetActive(false);
        _waitPanel.SetActive(false);
        _hintPanel.SetActive(false);
        GetComponent<GameManager>().isPlaying = true;
        AudioManager._audioManager.PlayClick();
        _panelsIsActive = false;
    }
    public void ShowWaitPanel()
    {
        _waitPanel.SetActive(true);
        GetComponent<GameManager>().isPlaying = false;
        _panelsIsActive = true;
        LanguageManager.S.RefreshAllTexts();
    }
    public void ShowWinPanel()
    {
        _winPanel.SetActive(true);
        GetComponent<GameManager>().isPlaying = false;
        AudioManager._audioManager.PlayClick();
        LanguageManager.S.RefreshAllTexts();
    }
    public void ContinuePlay()
    {
        _winPanel.SetActive(false);
        GetComponent<SaveController>().SetCurrentLevel(GetComponent<GameManager>().currentLvl + 1);
        if (GetComponent<GameManager>().currentLvl == 99)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            //AdvertismentManager.S.ShowInterstitial();
            SceneManager.LoadScene(1);
        }
        AudioManager._audioManager.PlayClick();
    }
    public void ShowHintPanel()
    {
        _hintPanel.SetActive(true);
        GetComponent<GameManager>().isPlaying = false;
        AudioManager._audioManager.PlayClick();
        _panelsIsActive = true;
        LanguageManager.S.RefreshAllTexts();
    }
    public void ShowHint()
    {
        GetComponent<GameManager>().isPlaying = true;
        _hintPanel.SetActive(false);
        //AdvertismentManager.S.ShowRevardedAd();
    }
    public void BackToMenu()
    {
        AudioManager._audioManager.PlayClick();
        Destroy(AudioManager._audioManager.gameObject);
        SceneManager.LoadScene(0);
    }
}
