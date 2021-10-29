using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksControl : MonoBehaviour
{
    [SerializeField] Transform shelfParent;
    [Space]
    [SerializeField] GameObject book;
    [SerializeField] LayerMask bookLayerMask;
    GameObject selectedBook1, selectedBook2;
    Vector3 selectedBookPosition1, selectedBookPosition2;    
    [SerializeField] Transform[] pointBook;
    [Space]
    [SerializeField] float speedSwap = 1.5f;
    bool isSwap = false;
    bool isSelect = true;
    readonly float rayDistance = 100;
    [Space]   
    #region CreateBooks
    public int createRedBooks = 4;
    public int createGreenBooks = 4;
    public int createBlueBooks = 4;
    #endregion


    #region Awake/Start/Update/FixedUpdate
    void Awake()
    {
        
    }
    void Start()
    {
        InstantiateBooks();
    }
    void Update()
    {
        SelectBook();     
    }
    void FixedUpdate()
    {
        SwapBooks();
    }
    #endregion
    void InstantiateBooks()
    {
        foreach (var point in pointBook)
        {
            Instantiate(book, point.position, Quaternion.identity, shelfParent);
        }
    }
    void SelectBook() 
    {
        if (Input.GetMouseButtonDown(0) && isSelect && !GameManager.Instance.isWin && !GameManager.Instance.isLose)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, rayDistance, bookLayerMask);

            if (hit.collider != null)
            {
                var hitBook = hit.collider.gameObject;

                if (selectedBook1 == null)
                {
                    selectedBook1 = hitBook;
                    selectedBook1.GetComponent<BookColor>().SelectColor(true);
                }
                else if (hitBook == selectedBook1)
                {
                    selectedBook1.GetComponent<BookColor>().SelectColor(false);
                    selectedBook1 = null;
                }
                else
                {
                    selectedBook2 = hitBook;
                    selectedBook1.GetComponent<BookColor>().SelectColor(false);
                    selectedBookPosition1 = selectedBook1.transform.localPosition;
                    selectedBookPosition2 = selectedBook2.transform.localPosition;
                    isSelect = false;
                    isSwap = true;
                }
            }
        }
    }
    void SwapBooks()
    {
        if (isSwap)
        {
            var checkPosition2 = new Vector3(selectedBook1.transform.localPosition.x, selectedBook1.transform.localPosition.y);
            var checkPosition1 = new Vector3(selectedBook2.transform.localPosition.x, selectedBook2.transform.localPosition.y);

            if (selectedBookPosition1 == checkPosition1 && selectedBookPosition2 == checkPosition2)
            {
                isSwap = false;
                selectedBook1 = null;
                selectedBook2 = null;
                GameManager.Instance.CheckBook();
                GameManager.Instance.numberMoves--;
                isSelect = true;
            }
            else
            {
                selectedBook1.transform.localPosition = Vector2.MoveTowards(selectedBook1.transform.localPosition, selectedBookPosition2, speedSwap * Time.deltaTime);
                selectedBook2.transform.localPosition = Vector3.MoveTowards(selectedBook2.transform.localPosition, selectedBookPosition1, speedSwap * Time.deltaTime);
            }
        }        
    }
}
