using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
  public static int Score;
  private Text _txtScore;

  void Start()
  {
    _txtScore = GetComponent<Text>();
  }

  public void UpdateScore(int points)
  {
    Score += points;
    _txtScore.text = Score.ToString();
  }

  public static void Reset()
  {
    Score = 0;
  }
}
