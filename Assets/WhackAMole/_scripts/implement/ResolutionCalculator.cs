using UnityEngine;

public class ResolutionCalculator : IExecuteAble
{
  private readonly Transform _topLeftTransform, _bottomRightTransform;
  private Vector3 _topLeft, _bottomRight, _pos, _temp;
  private readonly Transform[] _molePositions;
  private int _i;
  public ResolutionCalculator()
  {
    _topLeftTransform = WackAMoleControl.Instant.screenTopLeft;
    _bottomRightTransform = WackAMoleControl.Instant.screenBottomRight;
    _molePositions = WackAMoleControl.Instant.moles;
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
    _pos.x = (_bottomRight.x - _topLeft.x) / 3f;
    _pos.y = (_topLeft.y - _bottomRight.y) / 3f;
    _pos.z = (_bottomRight.z + _topLeft.z) / 2f;
    for (_i = 0; _i < _molePositions.Length; _i++) {
      switch (_i / 3)
      {
        case 0:
          _temp.x = ((_i % 3) + 0.5f) * _pos.x + _topLeft.x;
          _temp.y = _bottomRight.y;
          break;
        case 1:
          _temp.x = _topLeft.x;
          switch (_i % 3)
          {
            case 0:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y + (_pos.y / 2.5f);
              break;
            case 1:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y;
              break;
            default:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y - (_pos.y / 2.5f);
              break;
          }
          break;
        case 2:
          _temp.x = _bottomRight.x;
          switch (_i % 3)
          {
            case 0:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y + (_pos.y / 2.5f);
              break;
            case 1:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y;
              break;
            default:
              _temp.y = (((_i % 3) + 0.5f) * _pos.y) + _bottomRight.y - (_pos.y / 2.5f);
              break;
          }
          break;
        default:
          _temp = Vector3.zero;
          Debug.Log("element 0~8 only (9 moles) check the parameter!!!!");
          break;
      }
      _temp.z = _pos.z;
      _molePositions[_i].position = _temp;
    }
  }
}
