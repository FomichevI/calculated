
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class GameManager : MonoBehaviour
{
    public bool isPlaying = true;
    public int currentLvl;

    public Text mainCounterText;
    public Text supportCounterText;
    public Canvas canvas;
    public Transform bGTrans;
    public Animator counterAnimator;

    public GameObject startPrefab;
    public GameObject finishPrefab;
    public GameObject rockPrefab;
    public GameObject mainPrefab;

    public GameObject hand;

    private Square currentSqare;
    // столбец и строка текущего элемента в массиве
    private int currentColumn;
    private int currentLine;

    private Finish finishSqare;
    private Square lastSqare;
    private Square startSqare;
    private Square[,] squaresMat;

    private List<Square> possibleMoves;
    private List<Square> commitMoves;
    private List<string> correctPath; //список координат клеток

    private int currentHint = 0;
    private Square lastHintSquare;

    private int count;

    private bool isComplited;    

    void Start()
    {
        //загрузка текущего уровня из файла сохранения
        currentLvl = GetComponent<SaveController>().GetCurrentLevel();

        squaresMat = new Square[3, 5];

        commitMoves = new List<Square>();
        possibleMoves = new List<Square>();
        correctPath = new List<string>();

        counterAnimator = supportCounterText.GetComponent<Animator>();

        LoadLevel(currentLvl);
        if (currentLvl == 1) //показываем подсказку только на первом уровне
        {
            hand.SetActive(true);
            isPlaying = false;
            Invoke("ContinuePlay", 2.3f);
        }
    }

    private void ContinuePlay()
    {
        if (!GetComponent<LevelInterface>().PanelsIsActive) //если на данный момент нет активных панелей, перекрывающих уровень
            isPlaying = true;
    }

    private void SetPossibleMoves()
    {
        possibleMoves.Clear();    
        
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (squaresMat[i, j] == currentSqare)
                    {
                        currentColumn = i;
                        currentLine = j;
                        break;
                    }
                }
            }

        if (currentColumn != 0 && squaresMat[currentColumn - 1, currentLine] != null
            && !commitMoves.Contains(squaresMat[currentColumn - 1, currentLine]))
            possibleMoves.Add(squaresMat[currentColumn - 1, currentLine]);
        if (currentColumn != 2 && squaresMat[currentColumn + 1, currentLine] != null
            && !commitMoves.Contains(squaresMat[currentColumn + 1, currentLine]))
            possibleMoves.Add(squaresMat[currentColumn + 1, currentLine]);
        if (currentLine != 0 && squaresMat[currentColumn, currentLine - 1] != null
            && !commitMoves.Contains(squaresMat[currentColumn, currentLine - 1]))
            possibleMoves.Add(squaresMat[currentColumn, currentLine - 1]);
        if (currentLine !=4 && squaresMat[currentColumn, currentLine + 1] != null
            && !commitMoves.Contains(squaresMat[currentColumn, currentLine + 1]))
            possibleMoves.Add(squaresMat[currentColumn, currentLine + 1]);
    }

    private void Update()
    {
        if(isPlaying && Input.GetMouseButton(0))
        {
            hand.SetActive(false);
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
                    if (possibleMoves.Contains(hit2D.collider.GetComponent<Square>()))
                    {
                        if (currentSqare != null) //добавляем предыдущую ячейку к выбравнным, если она была
                        {
                            commitMoves.Add(currentSqare);
                            SetConnection(hit2D.collider.gameObject, false);
                        }
                        // устанавливаем текущую ячейку
                        currentSqare = hit2D.collider.GetComponent<Square>();
                        currentSqare.ShowLightning();

                        count += currentSqare.value;
                        SetCounterText();
                        SetPossibleMoves();

                        AudioManager._audioManager.PlayConnection();
                    }
                    //логика для движения в обратную сторону 
                    else if (commitMoves.Count >= 1 && commitMoves[commitMoves.Count - 1] == hit2D.collider.GetComponent<Square>())
                    {
                        Square sq = hit2D.collider.GetComponent<Square>();
                        currentSqare.HideAllLightnings();
                        commitMoves.Remove(sq);

                        count -= currentSqare.value;
                        currentSqare = sq;

                        SetCounterText();
                        SetPossibleMoves();

                        AudioManager._audioManager.PlayConnection();
                    }
                }
                else if(hit2D.collider.gameObject.tag == "Finish")
                {
                    if (currentSqare != null && currentSqare == lastSqare)
                    {
                        Finish fin = hit2D.collider.GetComponent<Finish>();
                        if (count == fin.value)
                        {
                            isComplited = true;
                            fin.ShowCorrectLightning();
                        }
                        else
                        {
                            fin.ShowIncorrectLightning();
                            Invoke("Refresh",0.3f);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            if (currentSqare != null)
            {
                Refresh();
                //заканчиваем уровень, если мы его прошли
                if (isComplited)
                {
                    GetComponent<LevelInterface>().ShowWinPanel();
                    GetComponent<SaveController>().SetMaxLevel(currentLvl + 1);
                    isComplited = false;
                }
            }
        }
    }
    private void Refresh()
    {
        if (currentSqare != null)
        {
            //снимаем все выделение
            foreach (Square sq in commitMoves)
                sq.HideAllLightnings();
            currentSqare.HideAllLightnings();
            finishSqare.HideLightning();
            //очищаем все списки
            commitMoves.Clear();
            currentSqare = null;
            possibleMoves.Clear();
            //устанавливаем возможной только клетку старта
            possibleMoves.Add(startSqare);
            //устанавливаем счетчик в 0
            count = 0;
            SetCounterText();
        }
    }

    private void SetCounterText()
    {
        mainCounterText.text = count.ToString();
        supportCounterText.text = count.ToString();
        counterAnimator.Play("New Animation");
    }

    public void ShowNewHint()
    {
        if (currentHint < correctPath.Count)
        {
            //взять необходимый квадрат из матрицы
            string[] matPos = correctPath[currentHint].Split('-');
            Square square = squaresMat[int.Parse(matPos[0]), int.Parse(matPos[1])];
            //подсветить его соединение в зависимости от положения последнего квадрата
            SetConnection(square.gameObject, true);
            //сделать квадрат последним в цепочке
            lastHintSquare = square;
            lastHintSquare.isHind = true;
            currentHint += 1;
            AudioManager._audioManager.PlayCorrect();
        }
    }


    private void SetConnection (GameObject newSquare, bool isHint)
    {
        if (!isHint) //если мы подсвечиваем не подсказку
        {
            if (newSquare.transform.position.y > currentSqare.transform.position.y)
                newSquare.GetComponent<Square>().ShowConnection(eConnectionNames.down);
            else if (newSquare.transform.position.y < currentSqare.transform.position.y)
                newSquare.GetComponent<Square>().ShowConnection(eConnectionNames.up);
            else if (newSquare.transform.position.x > currentSqare.transform.position.x)
                newSquare.GetComponent<Square>().ShowConnection(eConnectionNames.left);
            else if (newSquare.transform.position.x < currentSqare.transform.position.x)
                newSquare.GetComponent<Square>().ShowConnection(eConnectionNames.right);
        }
        else //если подсвечиваем подсказку
        {
            if (lastHintSquare == null) //определяем предыдущий квадрат, если его нет
                lastHintSquare = startSqare;

            if (newSquare.transform.position.y > lastHintSquare.gameObject.transform.position.y)
                newSquare.GetComponent<Square>().ShowHintConnection(eConnectionNames.down);
            else if (newSquare.transform.position.y < lastHintSquare.gameObject.transform.position.y)
                newSquare.GetComponent<Square>().ShowHintConnection(eConnectionNames.up);
            else if (newSquare.transform.position.x > lastHintSquare.gameObject.transform.position.x)
                newSquare.GetComponent<Square>().ShowHintConnection(eConnectionNames.left);
            else if (newSquare.transform.position.x < lastHintSquare.gameObject.transform.position.x)
                newSquare.GetComponent<Square>().ShowHintConnection(eConnectionNames.right);
        }
    }

    private void LoadLevel(int lvl)
    {
        TextAsset levelsText = Resources.Load<TextAsset>("XML/LevelsXML");
        XmlDocument levelX = new XmlDocument();
        levelX.LoadXml(levelsText.text);
        XmlNodeList lvlNodeX = levelX.GetElementsByTagName("level_" + lvl);
        XmlNodeList squaresNodesX = lvlNodeX[0].ChildNodes;
        foreach(XmlNode nodeX in squaresNodesX)
        {
            if (nodeX.Attributes["type"] != null)
            {
                if (nodeX.Attributes["type"].Value == "main")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(mainPrefab);
                    square.GetComponent<Square>().value = int.Parse(nodeX.Attributes["value"].Value);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(bGTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    squaresMat[culumn, line] = square.GetComponent<Square>();
                }

                else if (nodeX.Attributes["type"].Value == "start")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(startPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(bGTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    startSqare = square.GetComponent<Square>();
                    squaresMat[culumn, line] = startSqare;
                    possibleMoves.Add(startSqare);
                }

                else if (nodeX.Attributes["type"].Value == "finish")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(finishPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(bGTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), 615, 1);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                    finishSqare = square.GetComponent<Finish>();
                    finishSqare.value = int.Parse(nodeX.Attributes["value"].Value);
                    lastSqare = squaresMat[culumn, 0];
                }

                else if (nodeX.Attributes["type"].Value == "rock")
                {
                    //создаем объект
                    GameObject square = Instantiate<GameObject>(rockPrefab);
                    //временные данные
                    int culumn = int.Parse(nodeX.Attributes["culumn"].Value);
                    int line = int.Parse(nodeX.Attributes["line"].Value);
                    //настраиваем позицию
                    square.transform.SetParent(bGTrans);
                    Vector3 pos = new Vector3((-270 + culumn * 270), (360 - line * 270), 0);
                    square.transform.localScale = Vector3.one;
                    square.transform.localPosition = pos;
                }
            }
            else
            {
                string[] path = nodeX.Attributes["path"].Value.Split(' ');
                foreach (string i in path)
                    correctPath.Add(i);                
            }
        }
    }

}
