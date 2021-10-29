using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelfs : MonoBehaviour
{    
    public List<GameObject> listShelf;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        listShelf.Add(collision.gameObject);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        listShelf.Remove(collision.gameObject);
    }
}
