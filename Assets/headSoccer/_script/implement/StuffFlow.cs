using UnityEngine;

public class StuffFlow : IExecuteIntegerAble
{
  private readonly GameObject _title, _play, _result, _item, _endParticle;  
  private PlayState _state;
  private int _i;

  public StuffFlow()
  {
    _title = ControlMonoCatchStuff.Instant.uiRoot.GetChild(1).gameObject;
    _play = ControlMonoCatchStuff.Instant.uiRoot.GetChild(2).gameObject;
    _result = ControlMonoCatchStuff.Instant.uiRoot.GetChild(3).gameObject;
    _item = ControlMonoCatchStuff.Instant.holder.GetChild(4).gameObject;
    _endParticle = ControlMonoCatchStuff.Instant.particleRoot.GetChild(0).gameObject;
  }
  public void Execute(int param)
  {
    _state = (PlayState)param;
    _title.SetActive(_state == PlayState.title);
    _play.SetActive(_state == PlayState.play);
    _item.SetActive(_state == PlayState.play);
    _result.SetActive(_state == PlayState.result);
    _endParticle.SetActive(_state == PlayState.result);
    if (_state == PlayState.play)
    {
      for (_i = 0; _i < _item.transform.childCount; _i++) { _item.transform.GetChild(_i).gameObject.SetActive(false); }
    }
  }
}
