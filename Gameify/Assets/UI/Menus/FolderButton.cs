using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderButton : MonoBehaviour
{
    public Explorer explorer;
    public TextMeshProUGUI pathgui;
    public void OnClick()
    {
        explorer.displayPath(pathgui.text);
    }
}
