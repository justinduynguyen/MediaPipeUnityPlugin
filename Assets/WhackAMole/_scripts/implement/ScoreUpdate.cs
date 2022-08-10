using UnityEngine.UI;

public class ScoreUpdate : IExecuteBoolean
{
  private readonly Text _textScore;
  private int _score;

  public ScoreUpdate()
  {
    _textScore = WackAMoleControl.Instant.uiRoot.GetChild(0).GetChild(0).GetComponent<Text>();
    //Execute(true);
  }
  public void Execute(bool isParam)
  {
    if (isParam) { _score = 0; }//reset score
    else { _score += 1; }//add Score
    _textScore.text = _score.ToString();
  }
}
