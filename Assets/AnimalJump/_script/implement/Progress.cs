using UnityEngine;

public class Progress : IExecuteAble
{
  private readonly Transform _progress, _proGressTop, _progressBottom, _content;
  private float _lerp, _contentLocalY;
  public Progress()
  {
    _progress = Control.Instant.holderRoot.GetChild(2).GetChild(3);
    _proGressTop = Control.Instant.holderRoot.GetChild(2).GetChild(2);
    _progressBottom = Control.Instant.holderRoot.GetChild(2).GetChild(1);
    _content = Control.Instant.holderRoot.GetChild(1);
    _contentLocalY = 0f;
  }
  public void Execute()
  {
    if (_contentLocalY != _content.localPosition.y)
    {
      _contentLocalY = _content.localPosition.y;
      _lerp = Mathf.Clamp01(_contentLocalY / -560.7451f);
      _progress.localPosition = Vector3.Lerp(_progressBottom.localPosition, _proGressTop.localPosition, _lerp);
    }
  }
}
