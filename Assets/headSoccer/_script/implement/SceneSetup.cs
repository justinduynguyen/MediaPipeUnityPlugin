using UnityEngine;

public class SceneSetup : IExecuteAble
{
  private readonly Transform _topLeftTrans, _bottomRightTrans;
  private readonly Transform[] _sceneBorders;
  private readonly BoxCollider[] _boxColliders;
  private Vector3 _v3Pos, _v3Center, _v3Col, _topLeft, _bottomRight;
  private Vector2 _v2SceneSize;
  private int i;
  public SceneSetup()
  {
    _topLeftTrans = ControlMono.Instant.topLeft;
    _bottomRightTrans = ControlMono.Instant.bottomRight;
    _sceneBorders = new Transform[4];
    _boxColliders = new BoxCollider[4];
    _v3Col = Vector3.one * 2f;
    for (i = 0; i < _sceneBorders.Length; i++)
    {
      _sceneBorders[i] = ControlMono.Instant.transform.GetChild(i + 2);
      _sceneBorders[i].tag = i < 2 ? "SceneHorizontalBorder" : "SceneVerticalBorder";
      _boxColliders[i] = _sceneBorders[i].gameObject.AddComponent<BoxCollider>();
      switch (i)
      {
        case 0: _boxColliders[i].center = Vector3.left; break;
        case 1: _boxColliders[i].center = Vector3.right; break;
        case 2: _boxColliders[i].center = Vector3.up; break;
        default: _boxColliders[i].center = Vector3.down; break;
      }
    }
  }
  public SceneSetup(ControlMonoCatchStuff stuff)
  {
    _topLeftTrans = stuff.topLeft;
    _bottomRightTrans = stuff.bottomRight;
    _sceneBorders = new Transform[4];
    _boxColliders = new BoxCollider[4];
    _v3Col = Vector3.one * 2f;
    for (i = 0; i < _sceneBorders.Length; i++)
    {
      _sceneBorders[i] = stuff.transform.GetChild(i + 2);
      _sceneBorders[i].tag = i < 2 ? "SceneHorizontalBorder" : "SceneVerticalBorder";
      _boxColliders[i] = _sceneBorders[i].gameObject.AddComponent<BoxCollider>();
      switch (i)
      {
        case 0: _boxColliders[i].center = Vector3.left; break;
        case 1: _boxColliders[i].center = Vector3.right; break;
        case 2: _boxColliders[i].center = Vector3.up; break;
        default: _boxColliders[i].center = Vector3.down; break;
      }
    }
  }
  public void Execute()
  {
    if (_topLeft != _topLeftTrans.position || _bottomRight != _bottomRightTrans.position)
    {
      Debug.Log("Reset game scene");
      _topLeft = _topLeftTrans.position;
      _bottomRight = _bottomRightTrans.position;

      _v3Center = (_topLeft + _bottomRight) / 2f;
      _v2SceneSize.x = Mathf.Abs(_topLeft.x - _bottomRight.x);
      _v2SceneSize.y = Mathf.Abs(_topLeft.y - _bottomRight.y);
      _v3Pos.z = _v3Center.z;
      for (i = 0; i < _sceneBorders.Length; i++)
      {
        if (i < 2)
        {
          _v3Pos.x = i == 0 ? _topLeft.x : _bottomRight.x;
          _v3Pos.y = _v3Center.y;
          _v3Col.x = 2f;
          _v3Col.y = _v2SceneSize.y;
        }
        else
        {
          _v3Pos.x = _v3Center.x;
          _v3Pos.y = i == 2 ? _topLeft.y : _bottomRight.y;
          _v3Col.x = _v2SceneSize.x;
          _v3Col.y = 2f;
        }
        _sceneBorders[i].position = _v3Pos;
        _boxColliders[i].size = _v3Col;
      }
    }
  }
}
