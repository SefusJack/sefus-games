using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckButton : MonoBehaviour
{
    public Decks decks;
    public void OnClick()
    {
        decks.selectDeck(getDeckIndex());
    }
    public int getDeckIndex()
    {
        return transform.GetSiblingIndex();
    }
}
