using UnityEngine.UI;

public class SoccerScoreDisplace : IExecuteIntegerAble
{  
  private readonly Text _playScore, _resultScore;
  private int _score;
  private const int _WinScore = 10;

  public SoccerScoreDisplace()
  { 
    _playScore = ControlMono.Instant.uiRoot.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
    _resultScore = ControlMono.Instant.uiRoot.GetChild(3).GetChild(2).GetComponent<Text>();
  }
  public void Execute(int param)
  {
    _score = param;
    _playScore.text = "x" + _score.ToString();
    _resultScore.text = "SCORE: " + _score.ToString() + "/" + _WinScore.ToString();
  }
}
