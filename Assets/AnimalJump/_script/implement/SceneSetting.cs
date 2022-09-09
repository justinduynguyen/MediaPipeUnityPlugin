using UnityEngine;
public class SceneSetting : IExecuteAble
{
  private readonly Transform _holder, _tl, _br, _bg, _crop;
  private Vector3 _posTL, _posBR;
  public SceneSetting()
  {
    _crop = Control.Instant.transform.GetChild(2);
    _holder = Control.Instant.holderRoot;
    _tl = Control.Instant.topLeft;
    _br = Control.Instant.bottomRight;
    _bg = Control.Instant.bg;
    Execute();
  }
  public void Execute()
  {
    if (_posTL != _tl.position || _posBR != _br.position)
    {
      _posTL = _tl.position;
      _posBR = _br.position;
      _holder.position = (_posTL + _posBR) / 2f;
      _holder.localScale = Vector3.one * (Vector3.Distance(_posTL, _posBR) / 236.3247f);//236.3247f
      _bg.position = _holder.position;
      _bg.localScale = _holder.localScale;
      _crop.position = _holder.position;
      _crop.localScale = _holder.localScale;
      Control.Instant.sceneSize.x = _posBR.x - _posTL.x;
      Control.Instant.sceneSize.y = _posTL.y - _posBR.y;
    }
  }
}
