using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : IFallBlockAble
{
  private readonly Transform _topLeft, _bottomRight, _effect;
  private readonly IExecuteVector3Able _firework;
  private INodeAble _nodeTmp;
  private readonly List<INodeAble> _blocks;
  private Vector3 _tL, _bR, _center, _v3Tmp;
  private float _oneFifth, _distance;
  private const float _Speed = 50f;
  private int _code;
  public FallBlock()
  {
    _firework = Factory.CreateFireworks();
    _effect = MonoControl.Instant.holderRoot.GetChild(2);
    _topLeft = MonoControl.Instant.topLeft;
    _bottomRight = MonoControl.Instant.bottomRight;
    _blocks = new List<INodeAble>();
    for (var i = 0; i < 5; i++) { _blocks.Add(Factory.CreateNode(MonoControl.Instant.SpawnBlock().transform)); }
    Calculate();
    MonoControl.Instant.SetNodeCode(_code);
    Reset();
  }
  public void Fall()
  {
    Calculate();
    if (_blocks[0].IsBottom() == false)
    {
      _distance = _Speed * Time.deltaTime;
      for (var i = 0; i < _blocks.Count; i++) { _blocks[i].Move(Vector3.down * _distance); }
    }
    if (Input.GetKeyDown(KeyCode.F)) { Swap(); }
    GetBlockCode();
  }
  public void Reset()
  {
    for (var i = 0; i < _blocks.Count; i++)
    {
      _blocks[i].SetPosition((Vector3.right * _center.x) + (Vector3.forward * _center.z) + (Vector3.up * _tL.y) + (_oneFifth * i * Vector3.up));
    }
  }
  public void Swap()
  {
    _nodeTmp = _blocks[0];
    for (var i = 0; i < _nodeTmp.GetTransform().childCount; i++)
    {
      if (_nodeTmp.GetTransform().GetChild(i).gameObject.activeSelf)
      {
        _firework.Execute(_nodeTmp.GetTransform().GetChild(i).position);
      }
    }
    _blocks.RemoveAt(0);
    _nodeTmp.SetPosition(_blocks[^1].GetPosition() + (Vector3.up * _oneFifth));
    _blocks.Add(_nodeTmp);
  }
  private void Calculate()
  {
    if (_tL != _topLeft.position || _bR != _bottomRight.position)
    {
      _tL = _topLeft.position;
      _bR = _bottomRight.position;
      _center = (_tL + _bR) / 2f;
      _oneFifth = (_tL.y - _bR.y) / 5f;
      _v3Tmp = Vector3.one * (_bR.x - _tL.x) / 77.10466f;
      _effect.localScale = _v3Tmp;
      _effect.position = Vector3.right * _center.x + Vector3.up * _bR.y + Vector3.forward * _center.z;
      for (var i = 0; i < _blocks.Count; i++) { _blocks[i].SetScale(_v3Tmp); }
    }
  }
  public void GetBlockCode() {
    if (_blocks[0].GetPosition().y <= _topLeft.position.y)
    {
      if (_code != _blocks[0].GetCode())
      {
        _code = _blocks[0].GetCode();
        MonoControl.Instant.SetNodeCode(_code);
      }
    }
    else
    {
      if (_code != -2)
      {
        _code = -2;
        MonoControl.Instant.SetNodeCode(_code);
      }
    }
  }
}
