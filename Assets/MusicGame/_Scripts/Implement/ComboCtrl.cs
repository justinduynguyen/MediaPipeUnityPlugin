using UnityEngine;
using UnityEngine.UI;

public class ComboCtrl : IExecuteIntegerAble
{
  private int _combo;
  private readonly Text _textCombo;
  public ComboCtrl(Transform uiRoot) { _textCombo = uiRoot.GetChild(4).GetChild(1).GetComponent<Text>(); }
  public void Execute(int param)
  {
    _combo = param;
    _textCombo.text = "x" + _combo.ToString() + " Combo";
  }
}
