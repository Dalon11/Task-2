using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookColor : MonoBehaviour
{
    public enum ColorBook
    {
        Red,
        Green,
        Blue
    }
    public ColorBook color;
    public Color curentColor;
    public int id;


    #region Awake/Start/Update/FixedUpdate
    void Awake()
    {     
        
    }
    void Start()
    {        
        RandomColor();
        ColorsBooks();
    }
    void Update()
    {  
        
    }
    void FixedUpdate()
    {
       
    }
    #endregion
    void RandomColor()
    {
        color = (ColorBook)Random.Range(0, 3);        
    }
    void ColorsBooks()
    {
        var booksControl= GameManager.Instance.gameObject.GetComponent<BooksControl>();
        var renderer = GetComponent<SpriteRenderer>();   
        
        switch (color)
        {
            case ColorBook.Red:
                if (booksControl.createRedBooks != 0)
                {
                    renderer.color = Color.red;
                    booksControl.createRedBooks--;
                }
                else
                {
                    color++;
                    ColorsBooks();
                } 
                break;              
            case ColorBook.Green:
                if (booksControl.createGreenBooks != 0)
                {
                    renderer.color = Color.green;
                    booksControl.createGreenBooks--;
                }
                else
                {
                    color++;
                    ColorsBooks();
                }
                break;
            case ColorBook.Blue:
                if (booksControl.createBlueBooks != 0)
                {
                    renderer.color = Color.blue;
                    booksControl.createBlueBooks--;                    
                }
                else
                {
                    color = 0;
                    ColorsBooks();
                }
                break;
            default:
                break;
        }
        curentColor = GetComponent<SpriteRenderer>().color;
        id = (int)color;
        gameObject.name ="Book id " + id;
    }
    public void SelectColor(bool select)
    {
        var renderer = GetComponent<SpriteRenderer>();

        if (select) renderer.color = Color.yellow;
        else renderer.color = curentColor;
    }
}

