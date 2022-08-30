using UnityEngine.UI;

public class UiScore : IExecuteIntegerAble
{
  private readonly Text _playScore, _resultScore;
  public UiScore()
  {
    _playScore = Control.Instant.uiRoot.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
    _resultScore = Control.Instant.uiRoot.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>();
  }
  public void Execute(int param)
  {
    _playScore.text = "Score: " + param.ToString();
    _resultScore.text = param.ToString();
  }
}
