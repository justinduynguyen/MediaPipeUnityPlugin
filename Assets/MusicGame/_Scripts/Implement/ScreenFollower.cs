using UnityEngine;

public class ScreenFollower : IExecuteVector2Able
{
  private readonly Transform _follower, _screenTopLeft, _screenBottomRight;
  private Vector2 _oldParam;
  private Vector3 _position;
  private readonly bool _isLeft;
  private float _fltTmp;
  public ScreenFollower(Transform follower, Transform screenTopLeft, Transform screenBottomRight, bool isLeft)
  {
    _follower = follower;
    _screenTopLeft = screenTopLeft;
    _screenBottomRight = screenBottomRight;
    _isLeft = isLeft;
  }
  public void Execute(Vector2 param)
  {
    _position.z = (_screenTopLeft.position.z + _screenBottomRight.position.z) / 2f;
    _position.y = Mathf.Lerp(_screenBottomRight.position.y, _screenTopLeft.position.y, param.y);
    if (_isLeft)
    {
      _fltTmp = Mathf.Clamp(param.x, 0f, 0.5f);
      _position.x = Mathf.Lerp(_screenTopLeft.position.x, _screenBottomRight.position.x, _fltTmp);
      if (_fltTmp <= 0.05f || param.x < _oldParam.x - 0.005f) { MonoControl.Instant.CheckLeftSide(TargetCheck.left); }
      else if (_fltTmp >= 0.45f || param.x > _oldParam.x + 0.005f) { MonoControl.Instant.CheckLeftSide(TargetCheck.right); }
      if (param.y <= 0.1f || param.y < _oldParam.y - 0.005f) { MonoControl.Instant.CheckLeftSide(TargetCheck.bottom); }
      else if (param.y >= 0.8f || param.y > _oldParam.y + 0.005f) { MonoControl.Instant.CheckLeftSide(TargetCheck.top); }
    }
    else
    {
      _fltTmp = Mathf.Clamp(param.x, 0.5f, 1f);
      _position.x = Mathf.Lerp(_screenTopLeft.position.x, _screenBottomRight.position.x, _fltTmp);
      if (_fltTmp <= 0.55f || param.x < _oldParam.x - 0.005f) { MonoControl.Instant.CheckRightSide(TargetCheck.left); }
      else if (_fltTmp >= 0.95f || param.x > _oldParam.x + 0.005f) { MonoControl.Instant.CheckRightSide(TargetCheck.right); }
      if (param.y <= 0.1f || param.y < _oldParam.y - 0.005f) { MonoControl.Instant.CheckRightSide(TargetCheck.bottom); }
      else if (param.y >= 0.8f || param.y > _oldParam.y + 0.005f) { MonoControl.Instant.CheckRightSide(TargetCheck.top); }
    }
    _follower.position = _position;
    _oldParam = param;
  }
}
