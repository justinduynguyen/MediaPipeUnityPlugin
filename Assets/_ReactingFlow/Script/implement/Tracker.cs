using UnityEngine;

public class Tracker : IExecuteAble
{
  private readonly Transform _annotation;//15, 16 - 4
  private readonly GameObject _objectA, _objectB;
  private Vector3 _pos;

  public Tracker()
  {
    _annotation = Control.Instant.annotation;
    _objectA = Control.Instant.holder.GetChild(0).GetChild(0).gameObject;
    _objectB = Control.Instant.holder.GetChild(0).GetChild(1).gameObject;
  }
  public void Execute()
  {
    if (_annotation.childCount != 0)
    {
      if (_annotation.GetChild(0).gameObject.activeSelf)
      {
        Control.Instant.handA = AveragePos(15);
        Control.Instant.handB = AveragePos(16);
        if (_objectA.activeSelf == false)
        {
          _objectA.SetActive(true);
          _objectB.SetActive(true);
        }
        _objectA.transform.position = Control.Instant.handA;
        _objectB.transform.position = Control.Instant.handB;
      }
      else { ResetTrackerPos(); }
    }
    else { ResetTrackerPos(); }
  }
  private Vector3 AveragePos(int id)
  {
    for (var i = 0; i < 4; i++)
    {
      if (i == 0) { _pos = _annotation.GetChild(id).position; }
      else { _pos += _annotation.GetChild(id + (i * 2)).position; }
    }
    return _pos / 4f;
  }
  private void ResetTrackerPos()
  {
    if (Control.Instant.handA != Vector3.zero) { Control.Instant.handA = Vector3.zero; }
    if (Control.Instant.handB != Vector3.zero) { Control.Instant.handB = Vector3.zero; }
    if (_objectA.activeSelf)
    {
      _objectA.SetActive(false);
      _objectB.SetActive(false);
    }
  }
}
