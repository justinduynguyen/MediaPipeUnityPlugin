using System.Collections.Generic;
using UnityEngine;

public class StuffItemControl : IExecuteAble
{
  private readonly List<Stuff> _stuffs;
  private bool _isActivated;
  private float _duration;
  private Vector2 _durationMinMAx;
  private int _i;
  public StuffItemControl()
  {
    _stuffs = new List<Stuff>();
    _durationMinMAx = new Vector2(0.25f, 1f);
  }
  public void Execute()
  {
    _duration -= Time.deltaTime;
    if (_duration <= 0f)
    {
      _duration = Random.Range(_durationMinMAx.x, _durationMinMAx.y);
      _isActivated = false;
      for (_i = 0; _i < _stuffs.Count; _i++)
      {
        if (_stuffs[_i].Activate())
        {
          _isActivated = true;
          break;
        }
      }
      if (_isActivated == false) { _stuffs.Add(new Stuff(ControlMonoCatchStuff.Instant.CreateItem())); }
      _isActivated = _stuffs[^1].Activate();
    }
    for (_i = 0; _i < _stuffs.Count; _i++) { _stuffs[_i].Fall(); }
  }
}

public class Stuff
{
  private readonly Transform _topLeft, _bottomRight;
  private readonly GameObject _item;
  private float _fallSpeed, _roationSpeed;
  private int _id, _i;

  public Stuff(GameObject item)
  {
    _item = item;
    _topLeft = ControlMonoCatchStuff.Instant.topLeft;
    _bottomRight = ControlMonoCatchStuff.Instant.bottomRight;
  }
  public bool Activate()
  {
    if (_item.activeSelf)
    {
      return false;
    }
    else
    {
      _fallSpeed = Random.Range(5f, 10f);
      _roationSpeed = Random.Range(-360f, 360f);
      _item.SetActive(true);
      _id = Random.Range(0, _item.transform.childCount);
      for (_i = 0; _i < _item.transform.childCount; _i++) { _item.transform.GetChild(_i).gameObject.SetActive(_i == _id); }
      _item.transform.position = (Vector3.right * Random.Range(_topLeft.position.x + 0.6f, _bottomRight.position.x - 0.6f)) + (Vector3.up * _topLeft.position.y) + (Vector3.forward * _topLeft.position.z);
      return true;
    }
  }
  public void Fall()
  {
    if (_item.activeSelf)
    {
      _item.transform.Translate(_fallSpeed * Time.deltaTime * Vector3.down, Space.World);
      _item.transform.Rotate(0f, 0f, _roationSpeed * Time.deltaTime, Space.Self);
      if (_item.transform.position.y <= _bottomRight.transform.position.y) { _item.SetActive(false); }
    }
  }
}
