using UnityEngine;

public class Logic : IExecuteAble
{
  private bool _isStarted;
  private float _time, _intervalTime;
  private int _second;
  private readonly IExecuteAble _tracker, _idSwap, _sceneCtrl;
  private readonly IExecuteFloatAble[] _targets;
  public Logic()
  {
    _targets = new IExecuteFloatAble[Control.Instant.holder.GetChild(1).childCount];
    for (var i = 0; i< _targets.Length; i++) { _targets[i] = Factory.CreateTarget(i); }
    _tracker = Factory.CreateTracker();
    _idSwap = Factory.CreateIdSwap();
    _sceneCtrl = Factory.CreateSceneCtrl();
    _isStarted = false;
  }
  public void Execute()
  {
    _sceneCtrl.Execute();
    if (_isStarted == false) { ResetSession(); }
    else
    {
      _time -= Time.deltaTime;
      if (_time <= 0f) { EndSession(); }
      else
      {
        _tracker.Execute();
        if (_second != Mathf.RoundToInt(_time))
        {
          _second = Mathf.RoundToInt(_time);
          Control.Instant.SetTime(_second);
        }
        if (_intervalTime > 0f && Control.Instant.GetPlayable())
        {
          _intervalTime -= Time.deltaTime;
          if (_intervalTime <= 4f)
          {
            for (var i = 0; i < _targets.Length; i++) { _targets[i].Execute(_intervalTime / 4f); }
          }
        }
        else { ResetInterval(); }
      }
    }
  }
  private void ResetSession()
  {
    _isStarted = true;
    _time = 60f;
    _second = 60;    
    Control.Instant.SetTime(_second);
    Control.Instant.ResetSession();
    ResetInterval();
  }
  private void EndSession()
  {
    Debug.Log("Time out!!!!");
    _isStarted = false;
    Control.Instant.ChangeState();
  }
  private void ResetInterval()
  {
    Debug.Log("Reset.....");
    _intervalTime = 5f;
    _idSwap.Execute();
  }
}
