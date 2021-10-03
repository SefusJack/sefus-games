using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataViewer : MonoBehaviour
{
    bool includeheader = true;
    public Decks decks;
    public Explorer explorer;
    public GameObject cfab;
    public GameObject columnfab;
    public Transform colsspace;
    public GameObject dropfab;
    public Transform dropspace;
    public List<List<string>> colsdata = new List<List<string>>();

    public void Open()
    {
        colsdata = new List<List<string>>();
        for (int i = 0; i < 2; i++)
        {
            colsdata.Add(new List<string>());
            GameObject co = Instantiate(columnfab, new Vector3(0, 0, 0), Quaternion.identity);
            co.transform.SetParent(colsspace);
            co.SetActive(true);
            GameObject go = Instantiate(dropfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(dropspace);
            go.SetActive(true);
            displayColumn(0, i);
        }
    }

    public void deleteView()
    {
        foreach (Transform child in dropspace)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in colsspace)
        {
            Destroy(child.gameObject);
        }
    }

    public void displayColumn(int colnum, int cspacenum)
    {
        List<string> column = explorer.Column(colnum);
        colsdata[cspacenum] = column;
        Transform cspace = colsspace.GetChild(cspacenum).transform;
        foreach (Transform child in cspace)
        {
            Destroy(child.gameObject);
        }
        foreach (string cell in column)
        {
            GameObject go = Instantiate(cfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(cspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = cell;
            go.SetActive(true);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(colsspace.GetComponent<RectTransform>());
    }

    public void import()
    {
        Deck d = new Deck("");
        int i;
        if(includeheader)
        {
            i = 0;
        }
        else
        {
            i = 1;
        }
        while (i < colsdata[0].Count)
        {
            Card c = new Card(Row(i));
            d.Add(c);
            i++;
        }
        decks.addDeck(d);
    }
    public List<string> Row(int rownum)
    {
        List<string> row = new List<string>();
        for (int i = 0; i < colsdata.Count; i++)
        {
            row.Add(colsdata[i][rownum]);
        }
        return row;
    }
    public void setHeaderBool(bool b)
    {
        includeheader = b;
    }
}
