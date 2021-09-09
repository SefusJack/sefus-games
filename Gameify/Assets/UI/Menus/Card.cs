using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    List<string> info;
    public string title;
    public Card()
    {
        info = new List<string>();
        info.Add("");
        info.Add("");
    }
    public Card(List<string> list)
    {
        info = list;
        title = list[0];
    }
    public string getInfoLine(int index)
    {
        return info[index];
    }
    public void setInfoLine(int index, string s)
    {
        info[index] = s;
    }
    public List<string> getInfo(int index)
    {
        return info;
    }
    public void setInfo(List<string> i)
    {
        info = i;
    }
}
