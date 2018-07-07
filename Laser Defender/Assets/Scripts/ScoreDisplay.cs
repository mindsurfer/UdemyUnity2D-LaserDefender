using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
  public Text TextScore;

  private int _score;

  // Use this for initialization
  void Start () 
  {
    _score = ScoreManager.Score;
    TextScore.text = _score.ToString();
    ScoreManager.Reset();
  }
  
  // Update is called once per frame
  void Update () 
  {
    
  }
}
