using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
  [Header("UI Containers")]
  public TextMeshProUGUI questionText;
  public List<GameObject> answerHolder = new List<GameObject>();
  public GameObject quizPanel, resultPanel;
  public TextMeshProUGUI scoreText, correctAnswerText, wrongAnswerText;

  private int questionCounter = 0;
  [HideInInspector]
  public int wrongAnswerCounter = 0;
  [HideInInspector]
  public int correctAnswerCounter = 0;

  [Space]
  [Header("QuestionData")]
  public List<QuestionData> questionData = new List<QuestionData>();

  // Start is called before the first frame update
  void Start()
  {
    GenerateQuestion();
  }

  void SetAnswers()
  {
    for (int i = 0; i < answerHolder.Count; i++)
    {
      answerHolder[i].GetComponent<AnswerScript>().isCorrect = false;
      answerHolder[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questionData[questionCounter].answer[i];

      if (questionData[questionCounter].correctAnswer == i + 1)
      {
        answerHolder[i].GetComponent<AnswerScript>().isCorrect = true;
      }
    }
  }

  void GenerateQuestion()
  {
    questionText.text = questionData[questionCounter].question;
    SetAnswers();
  }

  public void Correct()
  {
    questionCounter++;
    if (questionCounter == 25)
    {
      var score = correctAnswerCounter * 4;

      correctAnswerText.text = correctAnswerCounter.ToString();
      wrongAnswerText.text = wrongAnswerCounter.ToString();
      scoreText.text = score.ToString();

      quizPanel.SetActive(false);
      resultPanel.SetActive(true);
    }
    else
    {
      GenerateQuestion();
    }
  }
}

[System.Serializable]
public class QuestionData
{
  public string question;
  public List<string> answer;
  public int correctAnswer;
}

