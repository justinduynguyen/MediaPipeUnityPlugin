using System.Collections.Generic;
using UnityEngine;

public class HandFollowControl : IExecuteAble
{
  private int _childCount, _i;
  private readonly Transform _handRectAnnotation;
  private readonly List<HandFollow> _followers;
  private readonly List<GameObject> _annotations;

  public HandFollowControl()
  {
    _annotations = new List<GameObject>();
    _childCount = 0;
    _handRectAnnotation = WackAMoleControl.Instant.rectListAnnotation;
    _followers = new List<HandFollow>();
  }
  public void Execute()
  {
    if (_handRectAnnotation.childCount != _childCount)
    {
      for (_i = 0; _i < _handRectAnnotation.childCount; _i++)
      {
        if (_annotations.Contains(_handRectAnnotation.GetChild(_i).gameObject) == false)
        {
          _followers.Add(new HandFollow(_handRectAnnotation.GetChild(_i)));
          _annotations.Add(_handRectAnnotation.GetChild(_i).gameObject);
        }
      }
      _childCount = _handRectAnnotation.childCount;
    }
    for (_i = 0; _i < _followers.Count; _i++) { _followers[_i].Follow(); }
  }
}

public class HandFollow
{
  private readonly Transform _followObject, _handAnnotation;
  private Vector3 _childPos;
  private int _i;

  public HandFollow(Transform handAnnotationChild)
  {
    _handAnnotation = handAnnotationChild;
    _followObject = WackAMoleControl.Instant.CreateFollowobject().transform;
    _followObject.gameObject.SetActive(true);
    _followObject.name = _handAnnotation.name + "_follower";
  }
  public void Follow()
  {
    if (_handAnnotation.parent.gameObject.activeSelf == false)
    {
      if (_followObject.gameObject.activeSelf) { _followObject.gameObject.SetActive(false); }
    }
    else
    {
      if (_followObject.gameObject.activeSelf != _handAnnotation.gameObject.activeSelf) { _followObject.gameObject.SetActive(_handAnnotation.gameObject.activeSelf); }
      if (_followObject.gameObject.activeSelf)
      {
        for (_i = 0; _i < _handAnnotation.GetChild(0).childCount; _i++)
        {
          if (_i <= 1 || (_i - 1) % 4 == 0) { _childPos += _handAnnotation.GetChild(0).GetChild(_i).position; }
        }
        _childPos /= 6;
        _followObject.position = _childPos;
        _childPos = Vector3.zero;
      }
    }
  }
}
