using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public GameDeck gamedeck;
    public void OnClick()
    {
        gamedeck.submitAnswer(getGameIndex());
    }
    public int getGameIndex()
    {
        return transform.GetSiblingIndex();
    }
}
