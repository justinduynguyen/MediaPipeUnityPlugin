using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : IExecuteAble
{
  private readonly Transform _frog;
  private readonly GameObject _crouch, _jump;
  private Vector3 _pos, _deltaTrackerPos, _oldTrack;
  private Vector2 _limitX;
  private float _minHeight, _frogWorldPositionX, _frogWorldPositionY, _verticleSpeed;
  private readonly float _jumpPower, _gravity;
  private readonly IBooleanAble _groundedCheck;
  public FrogMovement()
  {
    _frog = Control.Instant.holderRoot.GetChild(1).GetChild(1);
    _crouch = _frog.GetChild(0).gameObject;
    _jump = _frog.GetChild(1).gameObject;
    _oldTrack = Control.Instant.trackPos;
    _groundedCheck = Factory.CreateGroundCheck();
    _gravity = 9.8f * 100f;
    _jumpPower = _gravity * (1f / 4f);
  }
  public void Execute()
  {
    _frogWorldPositionX = CalculateHorizontal();
    _frogWorldPositionY = CalculateVertical();
    _pos = _frog.position;
    _pos.x = _frogWorldPositionX;
    _pos.y = _frogWorldPositionY;
    _frog.localPosition = _frog.parent.InverseTransformPoint(_pos);
  }
  private float CalculateVertical()
  {
    if (_minHeight != Control.Instant.sceneSize.y * Time.deltaTime * 5f) { _minHeight = Control.Instant.sceneSize.y * Time.deltaTime * 5f; }
    _deltaTrackerPos = Control.Instant.trackPos - _oldTrack;
    if (_groundedCheck.Check())
    {
      if (_verticleSpeed < 0f) { _verticleSpeed = 0f; }
      if (_deltaTrackerPos.y > _minHeight)
      {
        Debug.Log("Jump signal!!!");
        Control.Instant.PlayJumpSound();
        _verticleSpeed = _jumpPower * (Control.Instant.isPower ? 1.25f : 1f);
      }
      if (_jump.activeSelf)
      {
        _jump.SetActive(false);
        _crouch.SetActive(true);
      }
    }
    else
    {
      _verticleSpeed -= _gravity * Time.deltaTime;
      if (_crouch.activeSelf)
      {
        _jump.SetActive(true);
        _crouch.SetActive(false);
      }
    }
    _oldTrack = Control.Instant.trackPos;
    return _frog.position.y + (_verticleSpeed * Time.deltaTime);
  }
  private float CalculateHorizontal()
  {
    if (_limitX.x != Control.Instant.sceneSize.x / -2f)
    {
      _limitX.x = Control.Instant.sceneSize.x / -2f;
      _limitX.y = Control.Instant.sceneSize.x / 2f;
    }
    if (_frog.position.x != Control.Instant.trackPos.x) { return Mathf.Clamp(Control.Instant.trackPos.x, _limitX.x * 0.75f, _limitX.y * 0.75f); }
    else { return _frog.position.x; }
  }
}
