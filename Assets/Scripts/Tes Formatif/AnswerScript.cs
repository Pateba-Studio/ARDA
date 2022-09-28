using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
  [HideInInspector]
  public bool isCorrect = false;
  public QuizManager quizManager;

  public void Answer()
  {
    if (isCorrect)
    {
      quizManager.Correct();
      quizManager.correctAnswerCounter++;
    }
    else
    {
      quizManager.Correct();
      quizManager.wrongAnswerCounter++;
    }
  }
}
