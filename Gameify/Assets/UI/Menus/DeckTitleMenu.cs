using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckTitleMenu : MonoBehaviour
{
    public Decks decks;
    public GameObject decksmenu;
    public GameObject prevmenu;
    public GameObject decktitlemenu;
    public TMP_InputField dtitlefield;
    public TextMeshProUGUI dtitle;
    public bool saved = false;
    public void BackButton()
    {
        dtitle.text = "";
        if (saved)
        {
            prevmenu = decksmenu;
            decktitlemenu.SetActive(false);
            prevmenu.SetActive(true);
        }
        else
        {
            if (decks.newDeck)
            {
                decks.RemoveDeck(decks.curdeck);
                decktitlemenu.SetActive(false);
                prevmenu.SetActive(true);
                decks.displayDecks();
            }
            else 
            {
                decktitlemenu.SetActive(false);
                prevmenu.SetActive(true);
            }
        }
        saved = false;
        dtitlefield.text = "";
    }
    public void SaveButton()
    {
        if (decks.hasDeckTitle(decks.curdeck, dtitlefield.text))
        {
            dtitle.text = "This Title Already Exists";
            saved = false;
        }
        else
        {
            decks.setDeckTitle();
            decks.newDeck = false;
            dtitle.text = "Saved";
            saved = true;
        }
    }
    public void setPreviousMenu(GameObject pm)
    {
        prevmenu = pm;
    }
}
