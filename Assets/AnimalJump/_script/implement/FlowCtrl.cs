using UnityEngine;

public class FlowCtrl : IExecuteAble
{
  private FlowState _state;
  private readonly GameObject _title, _play, _result, _holder;
  private IExecuteAble _logic;
  public FlowCtrl()
  {
    _logic = Factory.CreateLogic();
    _state = FlowState.result;
    _title = Control.Instant.uiRoot.GetChild(0).gameObject;
    _play = Control.Instant.uiRoot.GetChild(1).gameObject;
    _result = Control.Instant.uiRoot.GetChild(2).gameObject;
    _holder = Control.Instant.holderRoot.gameObject;
    Execute();
  }
  public void Execute()
  {
    _state = (FlowState)(((int)_state + 1) % 3);
    _title.SetActive(_state == FlowState.title);
    _play.SetActive(_state == FlowState.play);
    _holder.SetActive(_state == FlowState.play);
    _result.SetActive(_state == FlowState.result);
    if (_state == FlowState.play) { Control.OnUpdate += _logic.Execute; }
    else if (_state == FlowState.result) { Control.OnUpdate -= _logic.Execute; }
  }
}
public enum FlowState { title = 0, play = 1, result = 2 }
