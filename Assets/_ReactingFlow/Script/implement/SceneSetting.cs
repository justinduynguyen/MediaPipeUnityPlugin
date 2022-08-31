using UnityEngine;

public class SceneSetting : IExecuteAble
{
  private readonly Transform _topleft, _bottomRight, _target;
  private Vector3 _tl, _br;//236.3306
  public SceneSetting()
  {
    _topleft = Control.Instant.topleft;
    _bottomRight = Control.Instant.bottomRight;
    _target = Control.Instant.holder.GetChild(1);
    _tl = _topleft.position;
    _br = _bottomRight.position;
    SetScene();
  }
  public void Execute()
  {
    if (_tl != _topleft.position || _br != _bottomRight.position)
    {
      _tl = _topleft.position;
      _br = _bottomRight.position;
      SetScene();
    }
  }
  private void SetScene()
  {
    _target.position = (_tl + _br) / 2f;
    Control.Instant.scale = Vector3.Distance(_tl, _br) / 236.3306f;
    _target.localScale = Control.Instant.scale * Vector3.one;
  }
}
