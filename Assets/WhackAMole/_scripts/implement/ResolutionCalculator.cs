using UnityEngine;

public class ResolutionCalculator : IExecuteAble
{
  private readonly Transform _topLeftTransform, _bottomRightTransform;
  private Vector3 _topLeft, _bottomRight, _posTmp;
  private readonly Transform[] _molePositions;
  private int _i;
  public ResolutionCalculator(Transform topLeft, Transform bottomRight, Transform[] moleTransform)
  {
    _topLeftTransform = topLeft;
    _bottomRightTransform = bottomRight;
    _molePositions = moleTransform;
    UpdateResolution();
  }
  public void Execute()
  {
    if (_topLeftTransform.position != _topLeft || _bottomRightTransform.position != _bottomRight) { UpdateResolution(); }
  }
  private void UpdateResolution()
  {
    _topLeft = _topLeftTransform.position;
    _bottomRight = _bottomRightTransform.position;
    _posTmp.x = (_bottomRight.x - _topLeft.x) / _molePositions.Length;
    _posTmp.y = _bottomRight.y;
    _posTmp.z = (_bottomRight.z + _topLeft.z) / 2f;
    for (_i = 0; _i < _molePositions.Length; _i++) { _molePositions[_i].position = ((((_i + 0.5f) * _posTmp.x) + _topLeft.x) * Vector3.right) + (Vector3.up * _posTmp.y) + (Vector3.forward * _posTmp.z); }
  }
}
