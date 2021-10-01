using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataViewer : MonoBehaviour
{
    public bool includeheader;
    public Decks decks;
    public Explorer explorer = new Explorer();
    public GameObject cfab;
    public Transform colsspace;
    public Transform cspacetwo;
    public GameObject dropfab;
    public Transform dropspace;
    List<List<string>> colsdata = new List<List<string>>();

    public void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject go = Instantiate(dropfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(dropspace);
            go.SetActive(true);
            colsdata.Add(new List<string>());
        }
        //displayColumn(1, cspacetwo);
    }
    public void displayColumn(int colnum, int cspacenum)
    {
        List<string> column = explorer.Column(colnum);
        colsdata[cspacenum] = column;
        Transform cspace = colsspace.GetChild(cspacenum).GetChild(0).transform;
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
}
