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
        List<string> header = explorer.getHeader();
        foreach (string field in header)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = field });
        }
        dropdown.RefreshShownValue();
        dataviewer.displayColumn(dropdown.value, transform.GetSiblingIndex());
    }

    public void displayColumn()
    {
        dataviewer.displayColumn(dropdown.value, transform.GetSiblingIndex());
    }
}
