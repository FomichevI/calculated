
using UnityEngine;
using UnityEngine.UI;

public enum eConnectionNames {up, down, right, left}


public class Square : MonoBehaviour
{
    public int value = 0; // значение 0 для клетки старта 
    public bool isHind;

    public GameObject lightning;
    public GameObject upConnection;
    public GameObject downConnection;
    public GameObject leftConnection;
    public GameObject rightConnection;

    public GameObject redPS;
    public GameObject greenPS;
    public GameObject yellowPS;

    public Image colorCenterImg;
    public Text mainText;
    public Text supportText;

    private Color plusSquereCol;
    private Color minusSquereCol;
    private Color startSquereCol;

    private Color mainConnectionCol;
    private Color hintConnectionCol;
    private eConnectionNames fixedConnection;

    void Start()
    {
        plusSquereCol = new Color(21/255f, 224/255f, 7/255f);
        minusSquereCol = new Color(224/255f, 39/255f, 7/255f);
        startSquereCol = new Color(1, 197 / 255f, 0);

        mainConnectionCol = new Color(1, 220 / 255f, 0);
        hintConnectionCol = new Color(21 / 255f, 224 / 255f, 7 / 255f);

        // устанавливаем цвет квадрата и текст
        if (value > 0)
        {
            SetCenterColor("plus");
            mainText.text = "+" + value;
            supportText.text = "+" + value;
        }
        else if (value < 0)
        {
            SetCenterColor("minus");
            mainText.text = value.ToString();
            supportText.text = value.ToString();
        }
        else
            SetCenterColor("start");
    }

    public void ShowLightning()
    {
        lightning.SetActive(true);

        if (value > 0)
            greenPS.SetActive(true);
        else if (value < 0)
            redPS.SetActive(true);
        else
            yellowPS.SetActive(true);
    }

    public void ShowConnection(eConnectionNames name)
    {
        switch (name)
        {
            case eConnectionNames.up:
                upConnection.SetActive(true);
                upConnection.GetComponent<Image>().color = mainConnectionCol;
                break;
            case eConnectionNames.down:
                downConnection.SetActive(true);
                downConnection.GetComponent<Image>().color = mainConnectionCol;
                break;
            case eConnectionNames.left:
                leftConnection.SetActive(true);
                leftConnection.GetComponent<Image>().color = mainConnectionCol;
                break;
            case eConnectionNames.right:
                rightConnection.SetActive(true);
                rightConnection.GetComponent<Image>().color = mainConnectionCol;
                break;
        }
    }

    public void ShowHintConnection(eConnectionNames name)
    {
        fixedConnection = name;
        switch (name)
        {
            case eConnectionNames.up:
                upConnection.SetActive(true);
                upConnection.GetComponent<Image>().color = hintConnectionCol;
                break;
            case eConnectionNames.down:
                downConnection.SetActive(true);
                downConnection.GetComponent<Image>().color = hintConnectionCol;
                break;
            case eConnectionNames.left:
                leftConnection.SetActive(true);
                leftConnection.GetComponent<Image>().color = hintConnectionCol;
                break;
            case eConnectionNames.right:
                rightConnection.SetActive(true);
                rightConnection.GetComponent<Image>().color = hintConnectionCol;
                break;
        }
    }

    public void SetCenterColor(string squareType)
    {
        switch (squareType)
        {
            case "plus":
                colorCenterImg.color = plusSquereCol;
                break;
            case "minus":
                colorCenterImg.color = minusSquereCol;
                break;
            case "start":
                colorCenterImg.color = startSquereCol;
                break;
        }
    }

    public void HideAllLightnings() //скрываем все связи, если квадрат не является подсказкой
    {
        lightning.SetActive(false);

        if (value > 0)
            greenPS.SetActive(false);
        else if (value < 0)
            redPS.SetActive(false);
        else
            yellowPS.SetActive(false);

        upConnection.SetActive(false);
        downConnection.SetActive(false);
        leftConnection.SetActive(false);
        rightConnection.SetActive(false);

        if (isHind)
            ShowHintConnection(fixedConnection);        
    }

}
