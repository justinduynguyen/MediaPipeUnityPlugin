using System.Collections.Generic;
using UnityEngine;

public class TrailItemControl : IExecuteAble, ITargetCheckAble
{
  private readonly Transform _far, _near;
  private readonly List<Item> _items;
  private Item _selected, _inPlace;
  private float _interval;
  private readonly float _speed;
  private Vector2 _intervalMinMax;
  private readonly bool _isleft;

  public TrailItemControl(Transform far, Transform near, bool isLeft)
  {
    _isleft = isLeft;
    _far = far;
    _near = near;
    _items = new List<Item>();
    _intervalMinMax = new Vector2(1.5f, 3f);
    _speed = 1f / 5f;
  }
  public void Execute()
  {
    _interval -= Time.deltaTime;
    if (_interval <= 0f)
    {
      _interval = Random.Range(_intervalMinMax.x, _intervalMinMax.y);
      _selected = null;
      for (var i = 0; i < _items.Count; i++)
      {
        if (_items[i].CheckActive() == false)
        {
          _selected = _items[i];
          break;
        }
      }
      if (_selected == null)
      {
        _selected = new Item(MonoControl.Instant.SpawnItem(_isleft), _far, _near);
        _items.Add(_selected);
      }
      _selected.Activate();
    }
    for (var i = 0; i < _items.Count; i++)
    {
      if (_items[i].CheckActive())
      {
        _items[i].AddLerpState(Time.deltaTime * _speed);
        if (_items[i] != _inPlace)
        {
          if (_items[i].CheckInPlace()) { _inPlace = _items[i]; }
        }
        else
        {
          if (_items[i].CheckInPlace() == false) { _inPlace = null; }
        }
      }
    }
  }
  public bool Check(TargetCheck target)
  {
    if (_inPlace == null)
    {
      return false;
    }
    else
    {
      if (_inPlace.CheckTarget(target))
      {
        MonoControl.Instant.Showparticle(_inPlace.GetDisplayTargetPosition());
        _inPlace = null;
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
