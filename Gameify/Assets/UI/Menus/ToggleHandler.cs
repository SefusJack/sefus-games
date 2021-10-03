using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    public DataViewer dataviewer = new DataViewer();
    public void setHeaderBool()
    {
        bool on = GetComponent<Toggle>().isOn;
        dataviewer.setHeaderBool(on);
    }
}
