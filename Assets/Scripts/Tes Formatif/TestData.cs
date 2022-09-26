using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestData : MonoBehaviour
{
  [Header("UI Containers")]
  public GameObject scrollView;
  public TextMeshProUGUI questionString;
  public GameObject buttonA, buttonB, buttonC, buttonD, buttonE;
  private int counter = 0;

  [Space]
  [Header("QuestionData")]
  public List<QuestionData> questionDatas = new List<QuestionData>();

  // Start is called before the first frame update
  void Start()
  {
    questionString.text = questionDatas[counter].question;

    buttonA.GetComponent<Button>().onClick.AddListener(() =>
    {
      counter++;
    });
    buttonB.GetComponent<Button>().onClick.AddListener(() =>
    {

    });
    buttonC.GetComponent<Button>().onClick.AddListener(() =>
    {

    });
    buttonD.GetComponent<Button>().onClick.AddListener(() =>
    {

    });
    buttonE.GetComponent<Button>().onClick.AddListener(() =>
    {

    });
  }

  // Update is called once per frame
  void Update()
  {
    questionString.text = questionDatas[counter].question;
  }
}

[System.Serializable]
public class QuestionData
{
  public string question;
  public List<AnswerData> data = new List<AnswerData>();
}

[System.Serializable]
public class AnswerData
{
  public string answer;
  public bool isTrue;
}
