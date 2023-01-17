using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

[RequireComponent(typeof(LevelInterface))]
public class GameManager : MonoBehaviour
{
    public bool IsPlaying = true;
    private int _currentLvl; public int CurrentLvl { get { return _currentLvl; } }

    [SerializeField] private Text _mainCounterText;
    [SerializeField] private Text _supportCounterText;
    [SerializeField] private Transform _panelTrans;

    [SerializeField] private GameObject _startPrefab;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private GameObject _mainPrefab;

    [SerializeField] private GameObject _hand;

    private Animator _counterAnimator;
    private Square _currentSqare;
    // столбец и строка текущего элемента в массиве
    private int _currentColumn;
    private int _currentLine;

    private Finish _finishSqare;
    private Square _startSqare;
    private Square[,] _allSquares; //двумерный массив всех Square
    private Square _lastSqare; //элемент последнего совершенного хода

    private List<Square> _possibleMoves; //список возможных ходов
    private List<Square> _commitedMoves; //список совершенных ходов
    private List<string> _correctPath; //список координат клеток

    private int _currentHint = 0;
    private Square _lastHintSquare;

    private int _count; //общий счет очков
    private bool _isComplited;

    void Start()
    {
        //загрузка текущего уровня из файла сохранения
        _currentLvl = SaveController.S.GetCurrentLevel();

        _allSquares = new Square[3, 5];
        _commitedMoves = new List<Square>();
        _possibleMoves = new List<Square>();
        _correctPath = new List<string>();
        _counterAnimator = _supportCounterText.GetComponent<Animator>();

        LoadLevel(_currentLvl);
        if (_currentLvl == 1) //показываем подсказку только на первом уровне
        {
            _hand.SetActive(true);
            IsPlaying = false;
            Invoke("ContinuePlay", 2.3f);
        }
    }
    private void ContinuePlay()
    {
        if (!GetComponent<LevelInterface>().PanelsIsActive) //если на данный момент нет активных панелей, перекрывающих уровень
            IsPlaying = true;
    }
    private void SetPossibleMoves() //метод, определяющий и устанавливающий все возможные направления ходов на данный момент
    {
        _possibleMoves.Clear();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (_allSquares[i, j] == _currentSqare)
                {
                    _currentColumn = i;
                    _currentLine = j;
                    break;
                }
            }
        }

        if (_currentColumn != 0 && _allSquares[_currentColumn - 1, _currentLine] != null
            && !_commitedMoves.Contains(_allSquares[_currentColumn - 1, _currentLine]))
            _possibleMoves.Add(_allSquares[_currentColumn - 1, _currentLine]);
        if (_currentColumn != 2 && _allSquares[_currentColumn + 1, _currentLine] != null
            && !_commitedMoves.Contains(_allSquares[_currentColumn + 1, _currentLine]))
            _possibleMoves.Add(_allSquares[_currentColumn + 1, _currentLine]);
        if (_currentLine != 0 && _allSquares[_currentColumn, _currentLine - 1] != null
            && !_commitedMoves.Contains(_allSquares[_currentColumn, _currentLine - 1]))
            _possibleMoves.Add(_allSquares[_currentColumn, _currentLine - 1]);
        if (_currentLine != 4 && _allSquares[_currentColumn, _currentLine + 1] != null
            && !_commitedMoves.Contains(_allSquares[_currentColumn, _currentLine + 1]))
            _possibleMoves.Add(_allSquares[_currentColumn, _currentLine + 1]);
    }
    private void Update()
    {
        if (IsPlaying && Input.GetMouseButton(0))
        {
            _hand.SetActive(false);
            Vector3 p;
            if (Input.GetKey(KeyCode.Mouse0))
                p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            else
                p = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            RaycastHit2D hit2D = Physics2D.Raycast(p, Vector2.zero); // Vector2.zero если нужен рейкаст именно под курсором

            if (hit2D.transform != null)
            {
                if (hit2D.collider.gameObject.tag == "Square")
                {
                    if (_possibleMoves.Contains(hit2D.collider.GetComponent<Square>())) //если ход является допустимым
                    {
                        if (_currentSqare != null) //если была выбранная предыдущая ячейка, то добавляем ее к выбранным
                        {
                            _commitedMoves.Add(_currentSqare);
                            ShowConnection(hit2D.collider.gameObject, false);
                        }
                        //устанавливаем текущую ячейку
                        _currentSqare = hit2D.collider.GetComponent<Square>();
                        _currentSqare.ShowLightning();

                        _count += _currentSqare.Value;
                        ShowCounterText();
                        SetPossibleMoves();

                        AudioManager.S.PlayConnection();
                    }
                    //логика для движения в обратную сторону 
                    else if (_commitedMoves.Count >= 1 && _commitedMoves[_commitedMoves.Count - 1] == hit2D.collider.GetComponent<Square>())
                    {
                        Square sq = hit2D.collider.GetComponent<Square>();
                        _currentSqare.HideAllLightnings();
                        _commitedMoves.Remove(sq);

                        _count -= _currentSqare.Value;
                        _currentSqare = sq;

                        ShowCounterText();
                        SetPossibleMoves();

                        AudioManager.S.PlayConnection();
                    }
                }
                else if (hit2D.collider.gameObject.tag == "Finish") //если текущий ход ведет к финишу
                {
                    if (_currentSqare != null && _currentSqare == _lastSqare)
                    {
                        Finish fin = hit2D.collider.GetComponent<Finish>();
                        if (_count == fin.Value)
                        {
                            _isComplited = true;
                            fin.ShowCorrectLightning();
                        }
                        else
                        {
                            fin.ShowIncorrectLightning();
                            Invoke("Refresh", 0.3f);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) //когда отпускаем кнопку
        {

            if (_currentSqare != null)
            {
                Refresh();
                //заканчиваем уровень, если мы его прошли
                if (_isComplited)
                {
                    GetComponent<LevelInterface>().ShowWinPanel();
                    SaveController.S.SetMaxLevel(_currentLvl + 1);
                    _isComplited = false;
                }
            }
        }
    }
    private void Refresh() //метод для возврата значений к старту уровня
    {
        if (_currentSqare != null)
        {
            //снимаем все выделение
            foreach (Square sq in _commitedMoves)
                sq.HideAllLightnings();
            _currentSqare.HideAllLightnings();
            _finishSqare.HideLightning();
            //очищаем все списки
            _commitedMoves.Clear();
            _currentSqare = null;
            _possibleMoves.Clear();
            //устанавливаем возможной только клетку старта
            _possibleMoves.Add(_startSqare);
            //устанавливаем счетчик в 0
            _count = 0;
            ShowCounterText();
        }
    }
    private void ShowCounterText()
    {
        _mainCounterText.text = _count.ToString();
        _supportCounterText.text = _count.ToString();
        _counterAnimator.Play("CounterAnimation");
    }
    public void ShowNewHint()
    {
        //подсказки показываются по одному ходу за раз последовательно
        if (_currentHint < _correctPath.Count)
        {
            //взять необходимый квадрат из матрицы
            string[] matPos = _correctPath[_currentHint].Split('-');
            Square square = _allSquares[int.Parse(matPos[0]), int.Parse(matPos[1])];
            //подсветить его соединение в зависимости от положения последнего квадрата
            ShowConnection(square.gameObject, true);
            //сделать квадрат последним в цепочке
            _lastHintSquare = square;
            _lastHintSquare.IsHintActive = true;
            _currentHint += 1;
            AudioManager.S.PlayCorrect();
        }
    }
    private void ShowConnection(GameObject newSquare, bool isHint)
    {
        if (!isHint) //если мы подсвечиваем не подсказку
        {
            if (newSquare.transform.position.y > _currentSqare.transform.position.y)
                newSquare.GetComponent<Square>().ShowConnection(ConnectionNames.down);
            else if (newSquare.transform.position.y < _currentSqare.transform.position.y)
                newSquare.GetComponent<Square>().ShowConnection(ConnectionNames.up);
            else if (newSquare.transform.position.x > _currentSqare.transform.position.x)
                newSquare.GetComponent<Square>().ShowConnection(ConnectionNames.left);
            else if (newSquare.transform.position.x < _currentSqare.transform.position.x)
                newSquare.GetComponent<Square>().ShowConnection(ConnectionNames.right);
        }
        else //если подсвечиваем подсказку
        {
            if (_lastHintSquare == null) //определяем предыдущий квадрат, если его нет
                _lastHintSquare = _startSqare;

            if (newSquare.transform.position.y > _lastHintSquare.gameObject.transform.position.y)
                newSquare.GetComponent<Square>().ShowHintConnection(ConnectionNames.down);
            else if (newSquare.transform.position.y < _lastHintSquare.gameObject.transform.position.y)
                newSquare.GetComponent<Square>().ShowHintConnection(ConnectionNames.up);
            else if (newSquare.transform.position.x > _lastHintSquare.gameObject.transform.position.x)
                newSquare.GetComponent<Square>().ShowHintConnection(ConnectionNames.left);
            else if (newSquare.transform.position.x < _lastHintSquare.gameObject.transform.position.x)
                newSquare.GetComponent<Square>().ShowHintConnection(ConnectionNames.right);
        }
    }
    private void LoadLevel(int lvl)
    {
        TextAsset levelsText = Resources.Load<TextAsset>("XML/LevelsXML");
        XmlDocument levelX = new XmlDocument();
        levelX.LoadXml(levelsText.text);
        XmlNodeList lvlNodeX = levelX.GetElementsByTagName("level_" + lvl);
        XmlNodeList squaresNodesX = lvlNodeX[0].ChildNodes;
        foreach (XmlNode nodeX in squaresNodesX)
        {
            if (nodeX.Attributes["type"] != null)
            {
                if (nodeX.Attributes["type"].Value == "main")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(_mainPrefab);
                    square.GetComponent<Square>().Value = int.Parse(nodeX.Attributes["value"].Value);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(_panelTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    _allSquares[culumn, line] = square.GetComponent<Square>();
                }

                else if (nodeX.Attributes["type"].Value == "start")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(_startPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(_panelTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    _startSqare = square.GetComponent<Square>();
                    _allSquares[culumn, line] = _startSqare;
                    _possibleMoves.Add(_startSqare);
                }

                else if (nodeX.Attributes["type"].Value == "finish")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(_finishPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(_panelTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), 615, 1);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    _finishSqare = square.GetComponent<Finish>();
                    _finishSqare.Value = int.Parse(nodeX.Attributes["value"].Value);
                    _lastSqare = _allSquares[culumn, 0];
                }

                else if (nodeX.Attributes["type"].Value == "rock")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(_rockPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(_panelTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                }
            }
            else
            {
                string[] path = nodeX.Attributes["path"].Value.Split(' ');
                foreach (string i in path)
                    _correctPath.Add(i);
            }
        }
    }
}
