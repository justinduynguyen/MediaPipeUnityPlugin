using UnityEngine;

public class BgScroll : IExecuteAble
{
  private float _deltaY, _distance;
  private readonly Transform _frog, _content;
  private readonly Transform[] _sideBGs;
  private readonly SpriteRenderer[] _borderSprite;
  private bool _boolTempt;
  public BgScroll()
  {
    _content = Control.Instant.holderRoot.GetChild(1);
    _frog = Control.Instant.holderRoot.GetChild(1).GetChild(1);
    _sideBGs = new Transform[Control.Instant.transform.GetChild(1).childCount];
    _borderSprite = new SpriteRenderer[Control.Instant.transform.GetChild(1).childCount * 2];
    for (var i = 0; i < _sideBGs.Length; i++) { 
      _sideBGs[i] = Control.Instant.transform.GetChild(1).GetChild(i);
      _borderSprite[i * 2] = _sideBGs[i].GetChild(0).GetComponent<SpriteRenderer>();
      _borderSprite[i * 2 + 1] = _sideBGs[i].GetChild(1).GetComponent<SpriteRenderer>();
      SwapSprite(i);
    }
  }
  public void Execute()
  {
    if (_deltaY != _frog.position.y - Camera.main.transform.position.y)
    {
      _deltaY = _frog.position.y - Camera.main.transform.position.y;
      _distance = 0f;
      if (_deltaY > -20f) { _distance = _deltaY + 20f; }
      else if (_deltaY < -47.7f) { _distance = 47.7f + _deltaY; }
      if (_distance != 0f)
      {
        _content.Translate(Vector3.down * _distance, Space.Self);
        for (var i = 0; i < _sideBGs.Length; i++)
        {
          _sideBGs[i].Translate(Vector3.down * _distance, Space.Self);
          if (_sideBGs[i].localPosition.y < -174 && _distance > 0f)
          {
            _sideBGs[i].localPosition = _sideBGs[i - 1 < 0 ? _sideBGs.Length - 1 : i - 1].localPosition + (Vector3.up * 116f);
            SwapSprite(i);
          }
          else if (_sideBGs[i].localPosition.y > 174 && _distance < 0f)
          {
            _sideBGs[i].localPosition = _sideBGs[(i + 1) % _sideBGs.Length].localPosition - (Vector3.up * 116f);
            SwapSprite(i);
          }
        }
        Control.Instant.ProgressDisplace();
      }
    }
  }
  private void SwapSprite(int id)
  {
    _boolTempt = Random.Range(0f, 100f) > 50f;
    _borderSprite[id * 2].sprite = _boolTempt ? Control.Instant.borderLeft: Control.Instant.borderRight;
    _borderSprite[id * 2].transform.localScale = ((_boolTempt ? Vector3.right : Vector3.left) + (Random.Range(0f, 100f) > 50f ? Vector3.up : Vector3.down) + Vector3.forward) * 11.3f;
    _boolTempt = Random.Range(0f, 100f) > 50f;
    _borderSprite[id * 2+1].sprite = _boolTempt ? Control.Instant.borderRight : Control.Instant.borderLeft;
    _borderSprite[id * 2 + 1].transform.localScale = ((_boolTempt ? Vector3.right : Vector3.left) + (Random.Range(0f, 100f) > 50f ? Vector3.up : Vector3.down) + Vector3.forward) * 11.3f;
  }
}
