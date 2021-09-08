using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{
    public Decks decks;
    public void OnClick()
    {
        decks.selectCard(getCardIndex());
    }
    public int getCardIndex()
    {
        return transform.GetSiblingIndex();
    }
}
