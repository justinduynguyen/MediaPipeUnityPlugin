using UnityEngine;

public class Flow : IExecuteAble
{
  private readonly GameObject _title, _play, _result, _holder;
  private FlowState _state;
  private IExecuteAble _logic;

  public Flow()
  {
    _title = Control.Instant.uiRoot.GetChild(0).gameObject;
    _play = Control.Instant.uiRoot.GetChild(1).gameObject;
    _result = Control.Instant.uiRoot.GetChild(2).gameObject;
    _holder = Control.Instant.holder.gameObject;
    _state = FlowState.title;
    _logic = Factory.CreateLogic();
    UpdateUI();
  }
  public void Execute()
  {
    _state = (FlowState)(((int)_state + 1) % 3);
    UpdateUI();
  }
  private void UpdateUI()
  {
    _title.SetActive(_state == FlowState.title);
    _play.SetActive(_state == FlowState.play);
    _holder.SetActive(_state == FlowState.play);
    _result.SetActive(_state == FlowState.result);
    if (_state == FlowState.play) { Control.OnCall += _logic.Execute; }
    else if (_state == FlowState.result) { Control.OnCall -= _logic.Execute; }
  }
}
public enum FlowState { title = 0, play = 1, result = 2 }
