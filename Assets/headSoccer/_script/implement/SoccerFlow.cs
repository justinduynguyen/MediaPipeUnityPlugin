using UnityEngine;

public class SoccerFlow : IExecuteIntegerAble
{
  private PlayState _state;
  private readonly GameObject _title, _play, _result, _holder, _win, _lose, _winParticle;
  public SoccerFlow()
  {
    _title = ControlMono.Instant.uiRoot.GetChild(1).gameObject;
    _play = ControlMono.Instant.uiRoot.GetChild(2).gameObject;
    _result = ControlMono.Instant.uiRoot.GetChild(3).gameObject;
    _holder = ControlMono.Instant.holder.gameObject;
    _win = ControlMono.Instant.uiRoot.GetChild(3).GetChild(0).gameObject;
    _lose = ControlMono.Instant.uiRoot.GetChild(3).GetChild(1).gameObject;
    _winParticle = ControlMono.Instant.particleRoot.GetChild(3).gameObject;
  }
  public void Execute(int param)
  {
    _state = (PlayState)param;
    _title.SetActive(_state == PlayState.title);
    _play.SetActive(_state == PlayState.play);
    _result.SetActive(_state == PlayState.result);
    _holder.SetActive(_state == PlayState.play);
    if (_state == PlayState.play) { ControlMono.Instant.Score(ControlMono.Instant.score * -1); }
    if (_state == PlayState.result)
    {
      _win.SetActive(ControlMono.Instant.score >= ControlMono.Instant.winScore);
      _lose.SetActive(ControlMono.Instant.score < ControlMono.Instant.winScore);
      _winParticle.SetActive(ControlMono.Instant.score >= ControlMono.Instant.winScore);
    }
    else { _winParticle.SetActive(false); }
  }
}
