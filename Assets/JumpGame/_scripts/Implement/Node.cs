using UnityEngine;

public class Node : INodeAble
{
  private readonly Transform _transform, _topLeft, _bottomRight;
  private readonly SpriteRenderer[] _spriteRenders;
  private readonly Color _clearColor;
  private float _colorLerp;
  private int _a, _b, _i, _code;

  public Node(Transform trans)
  {
    _topLeft = MonoControl.Instant.topLeft;
    _bottomRight = MonoControl.Instant.bottomRight;
    _clearColor = new Color(1f, 1f, 1f, 0f);
    _transform = trans;
    _spriteRenders = new SpriteRenderer[trans.childCount];
    for (var i = 0; i < trans.childCount; i++) { _spriteRenders[i] = trans.GetChild(i).GetComponent<SpriteRenderer>(); }
  }
  public int GetCode() {return _code; }
  public Vector3 GetPosition() { return _transform.position; }
  public void Move(Vector3 delta)
  {
    _transform.position += delta;
    _colorLerp = 1f - Mathf.Clamp01((_transform.position.y - _bottomRight.position.y) / (_topLeft.position.y - _bottomRight.position.y));
    for (_i = 0; _i < 4; _i++) { _spriteRenders[_i].color = Color.Lerp(_clearColor, Color.white, _colorLerp); }
  }

  public void SetPosition(Vector3 pos)
  {
    _transform.position = pos;
    _a = Random.Range(0, _spriteRenders.Length);
    _code = GetCodeValue(_a);
    _b = (_a + Random.Range(1, _spriteRenders.Length - 1)) % _spriteRenders.Length;
    _code += GetCodeValue(_b);
    for (_i = 0; _i < _spriteRenders.Length; _i++) { _spriteRenders[_i].gameObject.SetActive(_i == _a || _i == _b); }
  }
  public void SetScale(Vector3 scale) { _transform.localScale = scale; }
  public bool IsBottom() { return _transform.position.y <= _bottomRight.position.y; }
  private int GetCodeValue(int id)
  {
    switch (id)
    {
      case 0: return 1;
      case 1: return 2;
      case 2: return 4;
      default: return 7;
    }
  }

  public Transform GetTransform() { return _transform; }
}
