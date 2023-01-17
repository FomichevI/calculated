using UnityEngine;
using UnityEngine.UI;

public class MenuCreator : MonoBehaviour
{
    [SerializeField] private Transform _levelsContentTrans;
    [SerializeField] private GameObject _levelButtonPrefab;
    [SerializeField] private LevelButton _currentLevelBut;

    private Color _greenCol;
    private Color _redCol;
    private Color _yellowCol;

    void Start()
    {
        _greenCol = new Color(22 / 255f, 171 / 255f, 22 / 255f);
        _redCol = new Color(238 / 255f, 52 / 255f, 8 / 255f);
        _yellowCol = Color.clear;

        int maxCompleteLevel = GetComponent<SaveController>().GetMaxLevel();
        //меняем значение на большой кнопке с текущим уровнем
        _currentLevelBut.SetValue(maxCompleteLevel);

        for (int i = 1; i < 100; i++) //создаем список всех уровней
        {
            //создать кнопку
            GameObject button = Instantiate<GameObject>(_levelButtonPrefab);
            LevelButton levelButton = button.GetComponent<LevelButton>();
            button.transform.SetParent(_levelsContentTrans);
            button.transform.localScale = Vector3.one;
            //изменить ее порядковый номер
            levelButton.SetValue(i);
            //изменить цвет и активность в зависимости от порядкового номера 
            if (i < maxCompleteLevel)
                levelButton.SetImageColor(_greenCol);
            else if (i == maxCompleteLevel)
                levelButton.SetImageColor(_yellowCol);
            else
            {
                levelButton.SetImageColor(_redCol);
                button.GetComponent<Button>().interactable = false;
            }
        }
    }
}
