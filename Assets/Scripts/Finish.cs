using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [HideInInspector] public int Value;
    [SerializeField] private GameObject _lightning;
    [SerializeField] private GameObject _downConnection;
    [SerializeField] private Image _innerSquareImg;
    [SerializeField] private GameObject _sparksPS;
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _supportText;

    private Color _greenCol;
    private Color _redCol;

    private bool _isHighlighted;

    private void Start()
    {
        _greenCol = new Color(21 / 255f, 224 / 255f, 7 / 255f);
        _redCol = new Color(224 / 255f, 39 / 255f, 7 / 255f);
        _innerSquareImg.color = Color.clear;
        _mainText.text = Value.ToString();
        _supportText.text = Value.ToString();
    }

    public void ShowCorrectLightning()
    {
        if (!_isHighlighted)
        {
            _isHighlighted = true;
            _innerSquareImg.color = _greenCol;
            _downConnection.SetActive(true);
            _lightning.SetActive(true);
            _sparksPS.SetActive(true);
            AudioManager.S.PlayCorrect();
        }
    }

    public void ShowIncorrectLightning()
    {
        if (!_isHighlighted)
        {
            _isHighlighted = true;
            _innerSquareImg.color = _redCol;
            _downConnection.SetActive(true);
            _lightning.SetActive(true);
            AudioManager.S.PlayIncorrect();
        }
    }

    public void HideLightning()
    {
        _isHighlighted = false;
        _innerSquareImg.color = Color.clear;
        _downConnection.SetActive(false);
        _lightning.SetActive(false);
        _sparksPS.SetActive(false);
    }
}
