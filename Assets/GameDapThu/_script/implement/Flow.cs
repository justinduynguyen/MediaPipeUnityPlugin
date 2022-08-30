using UnityEngine;

public class Flow : IExecutable
{
  private FlowState _state;
  private readonly IExecutable _logic;
  private readonly GameObject _titleUI, _playUI, _resultUI, _holder;
  public Flow()
  {
    _state = FlowState.title;
    _logic = Factory.CreateLogic();
    _titleUI = Control.Instant.uiRoot.GetChild(0).gameObject;
    _playUI = Control.Instant.uiRoot.GetChild(1).gameObject;
    _resultUI = Control.Instant.uiRoot.GetChild(2).gameObject;
    _holder = Control.Instant.holderRoot.gameObject;
    SetFlow();
  }
  public void Execute()
  {
    _state = (FlowState)((((int)_state) + 1) % 3);
    SetFlow();
  }
  private void SetFlow()
  {
    _titleUI.SetActive(_state == FlowState.title);
    _playUI.SetActive(_state == FlowState.play);
    _resultUI.SetActive(_state == FlowState.result);
    _holder.SetActive(_state == FlowState.play);
    if (_state == FlowState.play)
    {
      Control.Instant.ResetUI();
      Control.OnCall += _logic.Execute;
    }
    else if(_state == FlowState.result)
    {
      Control.OnCall -= _logic.Execute;
    }
  }
}
public enum FlowState { title = 0, play = 1, result = 2 }
