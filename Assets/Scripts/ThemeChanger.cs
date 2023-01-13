
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeChanger : MonoBehaviour
{
    [SerializeField] private Sprite _frame1;
    [SerializeField] private Sprite _frame2;

    [SerializeField] private Image _mainBgImg;
    [SerializeField] private Image[] _stoneFramesImgs;
    [SerializeField] private Image[] _additionalBgImgs;
    [SerializeField] private TMP_Text[] _texts;

    private Color _mainBgTheme1Col;
    private Color _mainBgTheme2Col;
    private Color _mainBgTheme3Col;

    private Color _additionalBgTheme1Col;
    private Color _additionalBgTheme2Col;
    private Color _additionalBgTheme3Col;

    void Start()
    {
        _mainBgTheme1Col = new Color(65 / 255f, 65 / 255f, 65 / 255f);
        _mainBgTheme2Col = new Color(35 / 255f, 135 / 255f, 1);
        _mainBgTheme3Col = Color.white;
        _additionalBgTheme1Col = Color.white;
        _additionalBgTheme2Col = new Color(0, 190 / 255f, 230 / 255f);
        _additionalBgTheme3Col = _additionalBgTheme1Col;

        SetTheme(GetComponent<SaveController>().GetTheme());
    }

    public void SetTheme(int theme)
    {
        if (theme >= 1 && theme <= 3)
        {
            switch (theme)
            {
                case 1:
                    _mainBgImg.color = _mainBgTheme1Col;
                    foreach (Image im in _additionalBgImgs)
                        im.color = _additionalBgTheme1Col;
                    foreach (Image im in _stoneFramesImgs)
                        im.sprite = _frame1;
                    foreach (TMP_Text text in _texts)
                        text.color = Color.white;
                    break;
                case 2:
                    _mainBgImg.color = _mainBgTheme2Col;
                    foreach (Image im in _additionalBgImgs)
                        im.color = _additionalBgTheme2Col;
                    foreach (Image im in _stoneFramesImgs)
                        im.sprite = _frame2;
                    foreach (TMP_Text text in _texts)
                        text.color = Color.white;
                    break;
                case 3:
                    _mainBgImg.color = _mainBgTheme3Col;
                    foreach (Image im in _additionalBgImgs)
                        im.color = _additionalBgTheme3Col;
                    foreach (Image im in _stoneFramesImgs)
                        im.sprite = _frame1;
                    foreach (TMP_Text text in _texts)
                        text.color = Color.black;
                    break;
            }
        }

    }
}
