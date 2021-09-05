using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    string front;
    string back;
    
    public Card()
    {
        front = "";
        back = "";
    }
    public Card(string f, string b)
    {
        front = f;
        back = b;
    }

    public string getBack()
    {
        return back;
    }
    public string getFront()
    {
        return front;
    }
}
