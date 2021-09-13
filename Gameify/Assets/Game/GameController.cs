using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public virtual void RightAnswer()
    {
        Debug.Log("You're right");
    }
    public virtual void WrongAnswer()
    {
        Debug.Log("You're wrong");
    }
}
