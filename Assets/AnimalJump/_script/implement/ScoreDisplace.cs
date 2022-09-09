using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreDisplace : IExecuteIntAble
{
  private readonly Text _playScore, _resultScore;
  public ScoreDisplace()
  {
    _playScore = Control.Instant.uiRoot.GetChild(1).GetChild(3).GetComponent<Text>();
    _resultScore = Control.Instant.uiRoot.GetChild(2).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
  }
  public void Execute(int param)
  {
    _playScore.text = "x " + param.ToString();
    _resultScore.text = "X " + param.ToString();    
  }
}
