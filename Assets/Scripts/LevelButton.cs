
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int value;

    public Text mainText;
    public Text addedText;

    public Image centerImg;

    public void SetValue(int num)
    {
        value = num;
        mainText.text = value.ToString();
        addedText.text = value.ToString();
    }
    public void SetImageColor(Color color)
    {
        centerImg.color = color;
    }
    public void StartLevel()
    {
        AudioManager._audioManager.PlayClick();
        Camera.main.GetComponent<SaveController>().SetCurrentLevel(value);
        SceneManager.LoadScene(1);
    }

}
