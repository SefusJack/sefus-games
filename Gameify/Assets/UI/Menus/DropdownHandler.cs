using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DropdownHandler : MonoBehaviour
{
    public DataViewer dataviewer = new DataViewer();
    public Explorer explorer = new Explorer();
    public TMP_Dropdown dropdown;
    void Start()
    {
        int columncount =  explorer.getColumnCount();
        for (int i = 1; i < columncount+1; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = ("column " + i.ToString())});
        }
        dropdown.RefreshShownValue();
        dataviewer.displayColumn(dropdown.value, transform.GetSiblingIndex());
    }

    public void displayColumn()
    {
        dataviewer.displayColumn(dropdown.value, transform.GetSiblingIndex());
    }
}
