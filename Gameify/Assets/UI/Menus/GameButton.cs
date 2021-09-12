using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    public GameSelection game;
    public void OnClick()
    {
        game.selectScene(getGameIndex());
    }
    public int getGameIndex()
    {
        return transform.GetSiblingIndex();
    }
}
