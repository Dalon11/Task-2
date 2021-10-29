using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get { return _instance; }
    }
    static GameManager _instance;
    #endregion
    [SerializeField] Shelfs[] shelfs;
    [SerializeField] GameObject panel;
    [SerializeField] Text txtGame, txtMoves;
    [Space]
    public bool isWin = false;
    public bool isLose = false;
    public int numberMoves = 10;
    bool isFirstCheck = true;
   
    
    #region Awake/Start/Update/FixedUpdate
    void Awake()
    {
        #region Singlton
        if (_instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this;
        #endregion
    }
    void Start()
    {

    }
    void Update()
    {
        FirstCheck();
        TextMoves();
        Win();
        GameOver();        
    }
    void FixedUpdate()
    {
        
    }
    #endregion
    void Win()
    {
        if (isWin)
        {
            panel.SetActive(true);
            txtGame.text = "You Win!";
        }
    }
    void GameOver()
    {
        if (numberMoves <= 0 && !isWin)
        {
            panel.SetActive(true);
            txtGame.text = "You Lose!";
            isLose = true;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CheckBook()
    {
        for (int i = 0; i < shelfs.Length; i++)
        {
            foreach (var book in shelfs[i].listShelf)
            {
                if (book.GetComponent<BookColor>().id == shelfs[i].listShelf[0].GetComponent<BookColor>().id)
                {
                    isWin = true;
                }
                else
                {
                    isWin = false;
                    return;
                }
            }
        }
    }  
    void FirstCheck()
    {
        if (isFirstCheck)
        {
            for (int i = 0; i < shelfs.Length; i++)
            {
                foreach (var book in shelfs[i].listShelf)
                {
                    if (book == null) return;
                }
            }
            CheckBook();
            isFirstCheck = false;
        }
    }    
    void TextMoves()
    {
        txtMoves.text = numberMoves.ToString();
    }
}
