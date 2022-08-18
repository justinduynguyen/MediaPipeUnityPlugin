using System.Collections.Generic;
using UnityEngine;

public class HandTrack : IExecuteAble
{
  private readonly Transform _handAnnotationRoot, _topLeft, _bottomRight;
  private readonly List<Transform> _handFollowers;
  private readonly GameObject _followerRoot;
  private Vector3 _v3Tmp;
  private Vector2 _v2Tmp;
  private readonly List<Vector2> _v2Cordinates;
  private readonly IExecuteVector2Able _leftFollower, _rightFollower;
  private int _i;
  public HandTrack(Transform annotationRoot, Transform holder, Transform topLeft, Transform bottomRight)
  {
    _followerRoot = holder.GetChild(0).gameObject;
    _handAnnotationRoot = annotationRoot.GetChild(0).GetChild(2);
    _leftFollower = Factory.CreateScreenFollower(holder.GetChild(1).GetChild(0).GetChild(2), holder.GetChild(1).GetChild(0).GetChild(0), holder.GetChild(1).GetChild(0).GetChild(1), true);
    _rightFollower = Factory.CreateScreenFollower(holder.GetChild(1).GetChild(0).GetChild(3), holder.GetChild(1).GetChild(0).GetChild(0), holder.GetChild(1).GetChild(0).GetChild(1), false);
    _handFollowers = new List<Transform>();
    _topLeft = topLeft;
    _bottomRight = bottomRight;
    _v2Cordinates = new List<Vector2>();
  }
  public void Execute()
  {
    if (_followerRoot.activeSelf != _handAnnotationRoot.gameObject.activeSelf) { _followerRoot.SetActive(_handAnnotationRoot.gameObject.activeSelf); }
    if (_followerRoot.activeSelf)
    {
      if (_handAnnotationRoot.childCount > 0)
      {
        while (_handFollowers.Count < _handAnnotationRoot.childCount) { _handFollowers.Add(MonoControl.Instant.GetHandFollower().transform); }
        _v2Cordinates.Clear();
        for (_i = 0; _i < _handAnnotationRoot.childCount; _i++)
        {
          _handFollowers[_i].gameObject.SetActive(_handAnnotationRoot.GetChild(_i).gameObject.activeSelf);
          if (_handFollowers[_i].gameObject.activeSelf)
          {
            _handFollowers[_i].position = GetTrack(_handAnnotationRoot.GetChild(_i).GetChild(0));
            _v2Tmp.x = Mathf.Clamp01((_handFollowers[_i].position.x - _topLeft.position.x) / (_bottomRight.position.x - _topLeft.position.x));
            _v2Tmp.y = Mathf.Clamp01((_handFollowers[_i].position.y - _bottomRight.position.y) / (_topLeft.position.y - _bottomRight.position.y));
            _v2Cordinates.Add(_v2Tmp);
          }
        }
        if (_v2Cordinates.Count > 0) { MoveScreenFollower(); }
      }
    }
  }
  private Vector3 GetTrack(Transform annotationHand)
  {
    _v3Tmp = annotationHand.GetChild(0).position;
    _v3Tmp += annotationHand.GetChild(5).position;
    _v3Tmp += annotationHand.GetChild(9).position;
    _v3Tmp += annotationHand.GetChild(13).position;
    _v3Tmp += annotationHand.GetChild(17).position;
    _v3Tmp /= 5;
    return _v3Tmp;
  }
  private void MoveScreenFollower()
  {
    if (_v2Cordinates.Count == 1)
    {
      if (_v2Cordinates[0].x < 0.5f) { _leftFollower.Execute(_v2Cordinates[0]); }
      else { _rightFollower.Execute(_v2Cordinates[0]); }
    }
    else
    {
      if (_v2Cordinates[0].x < _v2Cordinates[1].x)
      {
        _leftFollower.Execute(_v2Cordinates[0]);
        _rightFollower.Execute(_v2Cordinates[1]);
      }
      else
      {
        _rightFollower.Execute(_v2Cordinates[0]);
        _leftFollower.Execute(_v2Cordinates[1]);
      }
    }
  }
}
