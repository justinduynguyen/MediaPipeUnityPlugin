using UnityEngine.UI;

public class UiLife : IExecuteIntegerAble
{
  private readonly Image[] _lifes;
  public UiLife()
  {
    _lifes = new Image[Control.Instant.uiRoot.GetChild(1).GetChild(2).childCount];
    for (var i = _lifes.Length-1; i >= 0; i--) { _lifes[i] = Control.Instant.uiRoot.GetChild(1).GetChild(2).GetChild(i).GetComponent<Image>(); }
  }
  public void Execute(int param)
  {
    for (var i = 0; i < _lifes.Length; i++) { _lifes[i].sprite = i < param ? Control.Instant.hearts[0] : Control.Instant.hearts[1]; }
  }
}
