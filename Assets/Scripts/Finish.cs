
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public int value;

    public GameObject lightning;
    public GameObject downConnection;
    public Image innerSquareImg;

    public GameObject sparksPS;

    public Text mainText;
    public Text supportText;

    private Color greenCol;
    private Color redCol;

    private bool isLightning;

    private void Start()
    {
        greenCol = new Color(21 / 255f, 224 / 255f, 7 / 255f);
        redCol = new Color(224 / 255f, 39 / 255f, 7 / 255f);
        innerSquareImg.color = Color.clear;
        mainText.text = value.ToString();
        supportText.text = value.ToString();
    }

    public void ShowCorrectLightning()
    {
        if (!isLightning)
        {
            isLightning = true;
            innerSquareImg.color = greenCol;
            downConnection.SetActive(true);
            lightning.SetActive(true);
            sparksPS.SetActive(true);
            AudioManager._audioManager.PlayCorrect();
        }
    }

    public void ShowIncorrectLightning()
    {
        if (!isLightning)
        {
            isLightning = true;
            innerSquareImg.color = redCol;
            downConnection.SetActive(true);
            lightning.SetActive(true);
            AudioManager._audioManager.PlayIncorrect();
        }
    }

    public void HideLightning()
    {
        isLightning = false;
        innerSquareImg.color = Color.clear;
        downConnection.SetActive(false);
        lightning.SetActive(false);
        sparksPS.SetActive(false);
    }

}
