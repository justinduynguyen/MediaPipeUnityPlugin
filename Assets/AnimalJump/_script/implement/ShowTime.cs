using UnityEngine.UI;

public class ShowTime : IExecuteIntAble
{
  private readonly Text _clock;
  public ShowTime() { _clock = Control.Instant.uiRoot.GetChild(1).GetChild(1).GetComponent<Text>(); }
  public void Execute(int param) { _clock.text = (param / 60).ToString() + ":" + (param % 60).ToString("00"); }
}
