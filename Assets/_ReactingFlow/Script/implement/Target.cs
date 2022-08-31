using UnityEngine;

public class Target : IExecuteFloatAble
{
  private readonly int _targetID;
  private readonly Transform _ring;
  private readonly GameObject _gameObject;
  private float _distance, _scale;
  public Target(int id)
  {
    _targetID = id;
    _ring = Control.Instant.holder.GetChild(1).GetChild(_targetID).GetChild(0);
    _gameObject = Control.Instant.holder.GetChild(1).GetChild(_targetID).gameObject;
  }
  public void Execute(float param)
  {
    if (_gameObject.activeSelf)
    {
      _ring.transform.localScale = Vector3.Lerp(Vector3.one * 0.45f, Vector3.one, Mathf.Clamp01(param));
      if (CheckContact())
      {
        _gameObject.SetActive(false);
        Control.Instant.Touchtarget(_targetID);
      }
    }
  }
  private bool CheckContact()
  {
    if (_scale != Control.Instant.scale)
    {
      _scale = Control.Instant.scale;
      _distance = _scale * 11f;
    }
    return Vector3.Distance(_gameObject.transform.position, Control.Instant.handA) <= _distance || Vector3.Distance(_gameObject.transform.position, Control.Instant.handB) <= _distance;
  }
}
