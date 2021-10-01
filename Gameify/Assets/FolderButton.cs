using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FolderButton : MonoBehaviour
{
    public Explorer explorer = new Explorer();
    public TextMeshProUGUI pathgui = new TextMeshProUGUI();
    public void OnClick()
    {
        explorer.displayPath(pathgui.text);
    }
}
