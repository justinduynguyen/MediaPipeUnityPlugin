using UnityEngine;

public class Flow : IExecuteAble
{
  private readonly GameObject _title, _play, _result, _holder;
  private FlowState _state;
  private readonly IExecuteAble _logic;
  
  public Flow()
  {
    _logic = Factory.CreateLogic();
    _title = MonoControl.Instant.uiRoot.GetChild(0).gameObject;
    _play = MonoControl.Instant.uiRoot.GetChild(1).gameObject;
    _result = MonoControl.Instant.uiRoot.GetChild(2).gameObject;
    _holder = MonoControl.Instant.holderRoot.gameObject;
    ChangeState(FlowState.title);
  }
  public void Execute() { ChangeState((FlowState)(((int)_state + 1) % 3)); }
  private void ChangeState(FlowState state)
  {
    _state = state;
    _title.SetActive(_state == FlowState.title);
    _play.SetActive(_state == FlowState.play);
    _result.SetActive(_state == FlowState.result);
    _holder.SetActive(_state == FlowState.play);
    if (_state == FlowState.play)
    {
      MonoControl.Instant.ResetSession();
      ((IResetAble)_logic).Reset();
      MonoControl.OnCalled += _logic.Execute;
    }
    else if (_state == FlowState.result)
    {
      MonoControl.OnCalled -= _logic.Execute;
    }
  }
}
public enum FlowState { title = 0, play = 1, result = 2 }
