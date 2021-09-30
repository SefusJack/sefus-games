using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Explorer : MonoBehaviour
{
    public GameObject filefab;
    public GameObject foldfab;
    public Transform fspace;
    string prevpath = "..";
    public void Start()
    {
        displayPath(System.IO.Directory.GetParent("./").FullName);
    }
    public void displayPath(string path)
    {
        foreach (Transform child in fspace)
        {
            Destroy(child.gameObject);
        }
        if (System.IO.Directory.GetParent(path).FullName != Path.GetPathRoot(path))
        {
            GameObject go = Instantiate(foldfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(fspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "..";
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = System.IO.Directory.GetParent(path).FullName;
            go.SetActive(true);
        }
        displayFolders(path);
        displayFiles(path);
    }
    public void displayFiles(string path)
    {
        string[] files = System.IO.Directory.GetFiles(path);
        foreach (string file in files)
        {
            GameObject go = Instantiate(filefab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(fspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Path.GetFileName(file);
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = file;
            go.SetActive(true);
        }
    }
    public void displayFolders(string path)
    {
        string[] folders = System.IO.Directory.GetDirectories(path);
        foreach (string folder in folders)
        {
            GameObject go = Instantiate(foldfab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(fspace);
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Path.GetFileName(folder);
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = folder;
            go.SetActive(true);
        }
    }
}
