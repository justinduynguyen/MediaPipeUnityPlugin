using System.Collections.Generic;
using UnityEngine;

public class FaceControl : IExecuteAble
{
  private readonly Transform _faceAnnotation, _follower, _topPlatform, _bottomPlartform, _topLeft, _bottRight;
  private int _faceCount, _i;
  private FaceObject _closestFace;
  private readonly List<FaceObject> _faces;
  private readonly List<Transform> _annotations;
  private Vector3 _v3Temp;

  public FaceControl()
  {
    _faceAnnotation = ControlMono.Instant.headAnnotation.transform;
    _follower = ControlMono.Instant.follower.transform;
    _topLeft = ControlMono.Instant.topLeft;
    _bottRight = ControlMono.Instant.bottomRight;
    _topPlatform = ControlMono.Instant.topPlatform;
    _bottomPlartform = ControlMono.Instant.bottomPlatform;
    _faces = new List<FaceObject>();
    _annotations = new List<Transform>();
  }
  public FaceControl(ControlMonoCatchStuff stuff)
  {
    _faceAnnotation = stuff.headAnnotation.transform;
    _follower = stuff.follower.transform;
    _topLeft = stuff.topLeft;
    _bottRight = stuff.bottomRight;
    _topPlatform = stuff.topPlatform;
    _bottomPlartform = stuff.bottomPlatform;
    _faces = new List<FaceObject>();
    _annotations = new List<Transform>();
  }
  public void Execute()
  {
    if (_closestFace != null) { _closestFace = null; }
    if (_faceAnnotation.gameObject.activeSelf)
    {
      if (_faceCount != _faceAnnotation.childCount)
      {
        _faceCount = _faceAnnotation.childCount;
        for (_i = 0; _i < _faceAnnotation.childCount; _i++)
        {
          if (_annotations.Contains(_faceAnnotation.GetChild(_i)) == false)
          {
            _faces.Add(new FaceObject(_faceAnnotation.GetChild(_i)));
            _annotations.Add(_faceAnnotation.GetChild(_i));
          }
        }
      }
      if (_faceCount > 0)
      {
        for (_i = 0; _i < _faces.Count; _i++)
        {
          if (_annotations[_i].gameObject.activeSelf)
          {
            if (_closestFace == null) { _closestFace = _faces[_i]; }
            else if (_closestFace.Size() < _faces[_i].Size()) { _closestFace = _faces[_i]; }
          }
        }
      }
    }
    if (_closestFace != null)
    {
      _follower.position = _closestFace.GetFollowerPosition();
      _v3Temp = _follower.position;
      _v3Temp.y = _topLeft.position.y;
      _topPlatform.position = _v3Temp;
      _v3Temp.y = _bottRight.position.y;
      _bottomPlartform.position = _v3Temp;
      if (_follower.gameObject.activeSelf == false) { _follower.gameObject.SetActive(true); }
    }
    else
    {
      if (_follower.gameObject.activeSelf) { _follower.gameObject.SetActive(false); }
    }
  }
}
public class FaceObject
{
  private readonly LineRenderer _frame;
  private readonly Transform _keyPointRoot;
  private Vector3 _pos;
  private int _i;

  public FaceObject(Transform annotation)
  {
    _frame = annotation.GetChild(0).GetComponent<LineRenderer>();
    _keyPointRoot = annotation.GetChild(1);
  }
  public float Size() { return Vector3.Distance(_frame.GetPosition(0), _frame.GetPosition(1)) * Vector3.Distance(_frame.GetPosition(1), _frame.GetPosition(2)); }
  public Vector3 GetFollowerPosition()
  {
    _pos = _keyPointRoot.GetChild(0).position;
    for (_i = 1; _i < _keyPointRoot.childCount; _i++) { _pos += _keyPointRoot.GetChild(_i).position; }
    return _pos /= _keyPointRoot.childCount;
  }
}
