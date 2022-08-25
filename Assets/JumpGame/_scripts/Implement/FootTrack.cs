using UnityEngine;

public class FootTrack : IExecuteAble
{
  private readonly Transform _annotation;
  private readonly GameObject _footA, _footB;
  private Vector3 _posA, _posB;
  private int _i;
  private bool _hasChild, _isTracking;
  private IExecuteVector2Able _code;
  private Vector2 _feetParam;

  public FootTrack()
  {
    _annotation = MonoControl.Instant.pointerAnnotation;
    _footA = MonoControl.Instant.SpawnFootFollower();
    _footB = MonoControl.Instant.SpawnFootFollower();
    _code = Factory.CreateFeetCode();
  }
  public void Execute()
  {
    if (_hasChild)
    {
      if (_isTracking!= _annotation.GetChild(0).gameObject.activeSelf)
      {
        _isTracking = _annotation.GetChild(0).gameObject.activeSelf;
      }
    }
    else
    {
      _hasChild = _annotation.childCount > 0;
    }
    if (_hasChild && _isTracking)
    {
      _posA = Vector3.zero;
      _posB = Vector3.zero;
      for (_i = 27; _i < _annotation.childCount; _i++)
      {
        if (_i % 2 == 0) { _posA += _annotation.GetChild(_i).position; }
        else { _posB += _annotation.GetChild(_i).position; }
      }
      _footA.transform.position = _posA / 3f;
      _footB.transform.position = _posB / 3f;
      if (_footA.activeSelf == false)
      {
        _footA.SetActive(true);
        _footB.SetActive(true);
      }
      _feetParam.x = _footA.transform.position.x;
      _feetParam.y = _footB.transform.position.x;
    }
    else
    {
      if (_footA.activeSelf)
      {
        _footA.SetActive(false);
        _footB.SetActive(false);
      }
      _feetParam = Vector2.zero;
    }
    _code.Execute(_feetParam);
  }
}
