using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    List<string> info;
    string title;
    public Card()
    {
        info = new List<string>();
    }
    public Card(List<string> list)
    {
        info = list;
    }
    public void Set(int index, string s)
    {
        info[index] = s;
    }
    public string Get(int index)
    {
        return info[index];
    }
    public void SetTitle(string s)
    {
        title = s;
    }
    public string GetTitle()
    {
        return title;
    }
}
