using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FileButton : MonoBehaviour
{
    public Explorer explorer = new Explorer();
    public TextMeshProUGUI pathgui = new TextMeshProUGUI();
    public void OnClick()
    {
        explorer.importCSV(pathgui.text);
    }
}
