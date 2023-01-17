using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    private int _value;
    [SerializeField] private Text _mainText;
    [SerializeField] private Text _addedText;
    [SerializeField] private Image _centerImg;

    public void SetValue(int num)
    {
        _value = num;
        _mainText.text = _value.ToString();
        _addedText.text = _value.ToString();
    }
    public void SetImageColor(Color color)
    {
        _centerImg.color = color;
    }
    public void StartLevel()
    {
        AudioManager.S.PlayClick();
        Camera.main.GetComponent<SaveController>().SetCurrentLevel(_value);
        SceneManager.LoadScene(1);
    }
}
