using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    List<string> info;
    public string title;
    public int correct = 0;
    public int wrong = 0;
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
    public void printInfo()
    {
        foreach (string s in info)
        {
            Debug.Log(s);
        }
    }
    public float getRatio()
    {
        if (correct == 0 && wrong == 0)
            return 0;
           
        int tempc = correct;
        if (correct == 0)
            tempc = 1;

        int tempw = wrong;
        if (wrong == 0)
            tempw = 1;

        return (float)tempc / (float)tempw;
    }
    public bool isMature()
    {
        if (getRatio() >= 1.5f)
            return true;
        else
            return false;
    }
    public bool isHard()
    {
        if (getRatio() < 0.5f && getRatio() > 0)
            return true;
        else
            return false;
    }
    public bool isNew()
    {
        if (correct == 0 && wrong == 0)
            return true;
        else
            return false;
    }
}
