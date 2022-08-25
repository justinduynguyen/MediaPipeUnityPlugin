using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayControl : IExecuteIntegerAble
{
  private int _score;
  private readonly Text _resultScore, _playScore;
  public ScoreDisplayControl(Transform uiRoot)
  {
    _resultScore = uiRoot.GetChild(5).GetChild(2).GetComponent<Text>();
    _playScore = uiRoot.GetChild(4).GetChild(3).GetChild(0).GetComponent<Text>();
  }
  public void Execute(int param)
  {
    _score = param;
    _playScore.text = "Score: " + _score.ToString();
    _resultScore.text = _score.ToString();
  }
}
