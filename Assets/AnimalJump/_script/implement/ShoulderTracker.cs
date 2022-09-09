using UnityEngine;

public class ShoulderTracker : IExecuteAble
{
  private readonly Transform _annotation, _follower, _tl, _br;
  private Vector3 _pos;
  public ShoulderTracker()
  {
    _annotation = Control.Instant.annotation;
    _follower = Control.Instant.holderRoot.GetChild(0).GetChild(0);
    _tl = Control.Instant.topLeft;
    _br = Control.Instant.bottomRight;
  }
  public void Execute()
  {
    if (_annotation.childCount <= 0)
    {
      if (_follower.gameObject.activeSelf) { _follower.gameObject.SetActive(false); }
    }
    else
    {
      if (_follower.gameObject.activeSelf == false) { _follower.gameObject.SetActive(true); }
      _pos = _annotation.GetChild(0).position;//(_annotation.GetChild(11).position + _annotation.GetChild(12).position) / 2f;
      _pos.x = Mathf.Clamp(_pos.x, _tl.position.x, _br.position.x);
      _pos.y = Mathf.Clamp(_pos.y, _br.position.y, _tl.position.y);
      _follower.position = _pos;
      Control.Instant.trackPos = _pos;
    }
  }
}
