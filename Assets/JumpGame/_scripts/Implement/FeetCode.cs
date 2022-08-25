using UnityEngine;

public class FeetCode : IExecuteVector2Able
{
  private readonly Transform _topLeft, _bottomRight;
  private readonly GameObject _posA, _posB, _posC, _posD;
  private float _oneEight, _oneForth, _left, _right;
  private int _footCode, _code;
  private Vector3 _oldTL, _oldBR;

  public FeetCode()
  {
    _topLeft = MonoControl.Instant.topLeft;
    _bottomRight = MonoControl.Instant.bottomRight;
    _posA = MonoControl.Instant.holderRoot.GetChild(2).GetChild(0).gameObject;
    _posB = MonoControl.Instant.holderRoot.GetChild(2).GetChild(1).gameObject;
    _posC = MonoControl.Instant.holderRoot.GetChild(2).GetChild(2).gameObject;
    _posD = MonoControl.Instant.holderRoot.GetChild(2).GetChild(3).gameObject;
    MonoControl.Instant.SetFootCode(-1);
    CalculatePosition();
  }
  public void Execute(Vector2 param)
  {
    CalculatePosition();
    if (param.magnitude > 0f)
    {
      _oneEight = (_bottomRight.position.x - _topLeft.position.x) / 8f;
      _oneForth = _oneEight * 2f;
      _posA.SetActive(CheckActive(0, param.x, param.y));
      _posB.SetActive(CheckActive(1, param.x, param.y));
      _posC.SetActive(CheckActive(2, param.x, param.y));
      _posD.SetActive(CheckActive(3, param.x, param.y));
      _footCode = (_posA.activeSelf ? 1 : 0) + (_posB.activeSelf ? 2 : 0) + (_posC.activeSelf ? 4 : 0) + (_posD.activeSelf ? 7 : 0);
    }
    else { _footCode = 0; }
    if (_code != _footCode)
    {
      _code = _footCode;
      MonoControl.Instant.SetFootCode(_code);
    }
  }
  private bool CheckActive(int id, float posXa, float posXb)
  {
    _left = ((id + 0.5f) * _oneForth) - _oneEight + _topLeft.position.x;
    _right = ((id + 0.5f) * _oneForth) + _oneEight + _topLeft.position.x;
    return (posXa > _left && posXa < _right) || (posXb > _left && posXb < _right);
  }
  private void CalculatePosition()
  {
    if (_oldTL != _topLeft.position || _oldBR != _bottomRight.position)
    {
      _oneEight = (_bottomRight.position.x - _topLeft.position.x) / 8f;
      _oneForth = _oneEight * 2f;
      _oldTL = _topLeft.position;
      _oldBR = _bottomRight.position;
    }
  }
}
