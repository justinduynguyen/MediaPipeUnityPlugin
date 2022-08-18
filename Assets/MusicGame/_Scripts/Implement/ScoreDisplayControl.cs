using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayControl : IExecuteIntegerAble
{
  private int _score;
  private readonly Text _textCombo;
  public ScoreDisplayControl(Transform uiRoot)
  {
    _textCombo = uiRoot.GetChild(5).GetChild(2).GetComponent<Text>();
  }
  public void Execute(int param)
  {
    _score = param;
    _textCombo.text = _score.ToString();
  }
}
