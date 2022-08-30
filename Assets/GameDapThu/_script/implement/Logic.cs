using UnityEngine;
using UnityEngine.UI;
public class Logic : IExecutable
{
  private Text _clock;
  private bool _isStarted;
  private readonly IExecutable[] _monsters;
  private readonly IExecutable _tracker;
  private float _time;
  private int _second;
  public Logic()
  {
    _clock = Control.Instant.uiRoot.GetChild(1).GetChild(3).GetComponent<Text>();
    _isStarted = false;
    _tracker = Factory.CreateTracker();
    _monsters = new IExecutable[5];
    for (var i = 0; i < _monsters.Length; i++) { _monsters[i] = Factory.CreatMonster(i); }
  }
  public void Execute()
  {
    if (_isStarted == false)
    {
      StartSession();
      for (var i = 0; i < _monsters.Length; i++) { Control.Instant.holderRoot.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false); }
    }
    else
    {
      _time -= Time.deltaTime;
      if (_second != Mathf.RoundToInt(_time))
      {
        _second = Mathf.RoundToInt(_time);
        _clock.text = _second.ToString() + "s";
      }
      _tracker.Execute();
      for (var i = 0; i < _monsters.Length; i++) { _monsters[i].Execute(); }
      if (Control.Instant.life < 1 || _time <= 0f) { EndSession(); }
    }
  }
  private void StartSession()
  {
    _isStarted = true;
    for (var i = 0; i < _monsters.Length; i++) { Control.Instant.holderRoot.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false); }
    _time = 45f;
    _second = 45;
    _clock.text = _second.ToString() + "s";
  }
  private void EndSession()
  {
    _isStarted = false;
    Control.Instant.NextFlowState();
  }
}
