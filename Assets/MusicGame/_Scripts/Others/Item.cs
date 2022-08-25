using UnityEngine;

public class Item
{
  private readonly Transform _far, _near;
  private readonly GameObject _object;
  private Transform _displaceTarget;
  private float _state;//0.87~0.945
  private int _intTmp;

  public Item(GameObject obj, Transform far, Transform near)
  {
    _near = near;
    _far = far;
    _state = 0f;
    _object = obj;
  }
  public float GetLerpState() { return _state; }
  public void AddLerpState(float delta)
  {
    _state += delta;
    if (_state >= 1f)
    {
      MonoControl.Instant.MissCombo();
      Deactivate();
    }
    else { Move(); }
  }
  public void Activate()
  {
    _state = 0f;
    _object.SetActive(true);
    _intTmp = Random.Range(0, _object.transform.childCount);
    for (var i = 0; i < _object.transform.childCount; i++)
    {
      _object.transform.GetChild(i).gameObject.SetActive(i == _intTmp);
    }
    _displaceTarget = _object.transform.GetChild(_intTmp);
    Move();
  }
  private void Move()
  {
    _object.transform.position = Vector3.Lerp(_far.transform.position, _near.transform.position, _state);
  }
  public void Deactivate()
  {
    _object.SetActive(false);
  }
  public bool CheckActive()
  {
    return _object.activeSelf;
  }
  public bool CheckInPlace()
  {
    return _state >= 0.7f && _state <= 0.88f;
  }
  public bool CheckTarget(TargetCheck target)
  {
    if (_object.transform.GetChild((int)target).gameObject.activeSelf)
    {
      Deactivate();
      return true;
    }
    else
    {
      return false;
    }
  }
  public Vector3 GetDisplayTargetPosition()
  {
    return _displaceTarget.position;
  }
}
