using UnityEngine;

public class FlowControl : IExecuteIntegerAble
{
  private GameObject _title, _play, _result, _miniScreen, _holder;
  private FlowState _state;
  public FlowControl(Transform uiRoot, Transform holder)
  {
    _miniScreen = uiRoot.GetChild(0).gameObject;
    _title = uiRoot.GetChild(3).gameObject;
    _play = uiRoot.GetChild(4).gameObject;
    _result = uiRoot.GetChild(5).gameObject;
    _holder = holder.gameObject;
  }
  public void Execute(int param)
  {
    _state = (FlowState)param;
    _title.SetActive(_state == FlowState.title);
    _play.SetActive(_state == FlowState.play);
    _miniScreen.SetActive(_state == FlowState.play);
    _holder.SetActive(_state == FlowState.play);
    _result.SetActive(_state == FlowState.result);
    Debug.Log(_state);
  }
}
