using UnityEngine;
using UnityEngine.UI;

public class CopyMiniScreen : ICheckAble
{
  private readonly RawImage _copied, _source;
  public CopyMiniScreen(Transform copy, Transform source)
  {
    _copied = copy.GetComponent<RawImage>();
    _source = source.GetComponent<RawImage>();
  }
  public bool Check()
  {
    if (_source.texture != null) { _copied.texture = _source.texture; }
    return _copied.texture != null;
  }
}
