using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileButton : MonoBehaviour
{
    public Explorer explorer;
    public TextMeshProUGUI pathgui;
    public void OnClick()
    {
        explorer.importDataFromCSV(pathgui.text);
    }
}
