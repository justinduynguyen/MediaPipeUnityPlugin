using UnityEngine.UI;
using UnityEngine;
public class SoccerTimeDisplace : IExecuteFloatAble
{
  private readonly Text _playPanelScore;
  private int _remainSeconds;
  public SoccerTimeDisplace()
  {
    _remainSeconds = 0;
    _playPanelScore = ControlMono.Instant.uiRoot.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
  }
  public void Execute(float param)
  {
    if (_remainSeconds != Mathf.FloorToInt(param))
    {
      _remainSeconds = Mathf.RoundToInt(param);
      _playPanelScore.text = _remainSeconds.ToString() + "S";
    }
  }
}
