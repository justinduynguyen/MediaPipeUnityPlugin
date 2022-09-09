using UnityEngine;

public class GameLogic : IExecuteAble
{
  private bool _isStarted, _isPlaying;
  private readonly IExecuteAble _frogMove, _platform, _bgScroll;
  private readonly IExecuteIntAble _clock, _scoreDisplace;
  private readonly IExecuteTransformAble _collectEffect;
  private float _playTime, _endingTime;
  private int _seconds, _score;
  private readonly Transform _content, _frog;
  public GameLogic()
  {
    _frogMove = Factory.CreateFrog();
    _clock = Factory.CreateClock();
    _scoreDisplace = Factory.CreateScoreDisplace();
    _platform = Factory.CreatePlatform();
    _bgScroll = Factory.CreateBgScroller();
    _collectEffect = Factory.CreateParticleCtrl(false);
    _content = Control.Instant.holderRoot.GetChild(1);
    _frog = Control.Instant.holderRoot.GetChild(1).GetChild(1);
  }
  public void Execute()
  {
    if (_isStarted == false) { StartSession(); }
    if (_isPlaying)
    {
      _playTime -= Time.deltaTime;
      if (_seconds != Mathf.RoundToInt(_playTime))
      {
        _seconds = Mathf.RoundToInt(_playTime);
        _clock.Execute(_seconds);
      }
      _frogMove.Execute();
      _bgScroll.Execute();
      if (Control.Instant.waterDrop != null)
      {
        if (Control.Instant.waterDrop.activeSelf)
        {
          if (Vector3.Distance(_frog.position, Control.Instant.waterDrop.transform.position) < 8f)
          {
            Control.Instant.waterDrop.SetActive(false);
            _collectEffect.Execute(Control.Instant.waterDrop.transform);
            _score += 1;
            _scoreDisplace.Execute(_score);
          }
        }
      }
      if (Control.Instant.isTop && Control.Instant.isOnground)
      {
        _isPlaying = false;
        Control.Instant.Finish();
      }
      else { _isPlaying = _playTime > 0f; }
    }
    else
    {
      if (_playTime <= 0f || _endingTime <= 0f) { EndSession(); }
      else { _endingTime -= Time.deltaTime; }
    }
  }
  private void StartSession()
  {
    Debug.Log("Start new game session....");
    Control.Instant.currentPlatform = 0;
    Control.Instant.waterDrop = null;
    Control.Instant.isTop = false;
    _platform.Execute();
    _isStarted = true;
    _isPlaying = true;
    _playTime = 60f;
    _seconds = 60;
    _score = 0;
    _endingTime = 0.75f;
    _content.localPosition = Vector3.zero;
    Control.Instant.ProgressDisplace();
    _frog.localPosition = Vector3.down * 47.7f;
    _clock.Execute(_seconds);
    _scoreDisplace.Execute(_score);
  }
  private void EndSession()
  {
    Debug.Log("End game session....");
    _isStarted = false;
    Control.Instant.ChangeFlow();
  }
}
