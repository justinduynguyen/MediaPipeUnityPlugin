using UnityEngine;

public class SceneSetting : IExecutable
{
  private readonly Transform _topleft, _bottomRight, _tunnelRoot, _foreGround;
  private float _distance;//236.2131
  public SceneSetting()
  {
    _topleft = Control.Instant.topLeft;
    _bottomRight = Control.Instant.bottomRight;
    _tunnelRoot = Control.Instant.holderRoot.GetChild(0);
    _foreGround = Control.Instant.foreGround;
    Execute();
  }
  public void Execute()
  {
    if (_distance != Vector3.Distance(_topleft.position, _bottomRight.position))
    {
      _distance = Vector3.Distance(_topleft.position, _bottomRight.position);
      _tunnelRoot.position = (_topleft.position + _bottomRight.position) / 2f;
      _foreGround.position = _tunnelRoot.position;
      _tunnelRoot.localScale = Vector3.one * _distance / 236.2131f;
      _foreGround.localScale = _tunnelRoot.localScale;
    }
  }
}
