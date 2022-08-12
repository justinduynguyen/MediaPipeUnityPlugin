using UnityEngine.UI;

public class StuffScoreDisplace : IExecuteIntegerAble
{
  private readonly Text _resultScore;
  public StuffScoreDisplace() { _resultScore = ControlMonoCatchStuff.Instant.uiRoot.GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>(); }
  public void Execute(int param) { _resultScore.text = param.ToString(); }
}
