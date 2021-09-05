using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getDecks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getDecks()
    {
        string line;
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader(@".\Decks\test.txt");
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            while (line != null)
            {
                //write the lie to console window
                Debug.Log(line);
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
    }
}
