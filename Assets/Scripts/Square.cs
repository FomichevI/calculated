
using UnityEngine;
using UnityEngine.UI;

public enum ConnectionNames { up, down, right, left }

public class Square : MonoBehaviour
{
    [HideInInspector] public int Value = 0; // значение 0 для клетки старта 
    [HideInInspector] public bool IsHintActive;

    [SerializeField] private GameObject _lightning;
    [SerializeField] private GameObject _upConnection;
    [SerializeField] private GameObject _downConnection;
    [SerializeField] private GameObject _leftConnection;
    [SerializeField] private GameObject _rightConnection;

    [SerializeField] private GameObject _redPS;
    [SerializeField] private GameObject _greenPS;
    [SerializeField] private GameObject _yellowPS;

    [SerializeField] private Image _colorCenterImg;
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _supportText;

    private Color _plusSquereCol;
    private Color _minusSquereCol;
    private Color _startSquereCol;

    private Color _mainConnectionCol;
    private Color _hintConnectionCol;
    private ConnectionNames _fixedConnection;

    void Start()
    {
        _plusSquereCol = new Color(21 / 255f, 224 / 255f, 7 / 255f);
        _minusSquereCol = new Color(224 / 255f, 39 / 255f, 7 / 255f);
        _startSquereCol = new Color(1, 197 / 255f, 0);

        _mainConnectionCol = new Color(1, 220 / 255f, 0);
        _hintConnectionCol = new Color(21 / 255f, 224 / 255f, 7 / 255f);

        //устанавливаем цвет квадрата и текст
        if (Value > 0)
        {
            SetCenterColor("plus");
            _mainText.text = "+" + Value;
            _supportText.text = "+" + Value;
        }
        else if (Value < 0)
        {
            SetCenterColor("minus");
            _mainText.text = Value.ToString();
            _supportText.text = Value.ToString();
        }
        else
            SetCenterColor("start");
    }

    public void ShowLightning()
    {
        _lightning.SetActive(true);

        if (Value > 0)
            _greenPS.SetActive(true);
        else if (Value < 0)
            _redPS.SetActive(true);
        else
            _yellowPS.SetActive(true);
    }

    public void ShowConnection(ConnectionNames name)
    {
        switch (name)
        {
            case ConnectionNames.up:
                _upConnection.SetActive(true);
                _upConnection.GetComponent<Image>().color = _mainConnectionCol;
                break;
            case ConnectionNames.down:
                _downConnection.SetActive(true);
                _downConnection.GetComponent<Image>().color = _mainConnectionCol;
                break;
            case ConnectionNames.left:
                _leftConnection.SetActive(true);
                _leftConnection.GetComponent<Image>().color = _mainConnectionCol;
                break;
            case ConnectionNames.right:
                _rightConnection.SetActive(true);
                _rightConnection.GetComponent<Image>().color = _mainConnectionCol;
                break;
        }
    }

    public void ShowHintConnection(ConnectionNames name)
    {
        _fixedConnection = name;
        switch (name)
        {
            case ConnectionNames.up:
                _upConnection.SetActive(true);
                _upConnection.GetComponent<Image>().color = _hintConnectionCol;
                break;
            case ConnectionNames.down:
                _downConnection.SetActive(true);
                _downConnection.GetComponent<Image>().color = _hintConnectionCol;
                break;
            case ConnectionNames.left:
                _leftConnection.SetActive(true);
                _leftConnection.GetComponent<Image>().color = _hintConnectionCol;
                break;
            case ConnectionNames.right:
                _rightConnection.SetActive(true);
                _rightConnection.GetComponent<Image>().color = _hintConnectionCol;
                break;
        }
    }

    public void SetCenterColor(string squareType)
    {
        switch (squareType)
        {
            case "plus":
                _colorCenterImg.color = _plusSquereCol;
                break;
            case "minus":
                _colorCenterImg.color = _minusSquereCol;
                break;
            case "start":
                _colorCenterImg.color = _startSquereCol;
                break;
        }
    }

    public void HideAllLightnings() //скрываем все связи, если квадрат не активирован как подсказка
    {
        _lightning.SetActive(false);

        if (Value > 0)
            _greenPS.SetActive(false);
        else if (Value < 0)
            _redPS.SetActive(false);
        else
            _yellowPS.SetActive(false);

        _upConnection.SetActive(false);
        _downConnection.SetActive(false);
        _leftConnection.SetActive(false);
        _rightConnection.SetActive(false);

        if (IsHintActive)
            ShowHintConnection(_fixedConnection);
    }
}
