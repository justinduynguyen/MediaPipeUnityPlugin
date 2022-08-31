using UnityEngine;

public class IdSwap : IExecuteAble
{
  private int _id, _intTmp;
  private int[] _ids;
  public IdSwap()
  {

    _ids = new int[Control.Instant.holder.GetChild(1).childCount];
    for (var i = 0; i < _ids.Length; i++) { _ids[i] = i; }
  }
  public void Execute()
  {
    if (Random.Range(0f, 100f) > 30f) { SwapRandom(); }
    else { SwapAntiClock(Random.Range(0, 2) > 0); }
    Control.Instant.SetIDs(_ids);
  }
  private void SwapAntiClock(bool isAntiClockWise)
  {
    _id = Random.Range(0, _ids.Length);
    for (var i = 0; i < _ids.Length; i++)
    {
      _ids[_id] = i;
      _id += isAntiClockWise ? 1 : -1;
      if (_id < 0) { _id = _ids.Length - 1; }
      else if (_id >= _ids.Length) { _id = 0; }
    }
  }
  private void SwapRandom()
  {
    for (var i = 0; i < _ids.Length - 1; i++)
    {
      _intTmp = _ids[i];
      _id = Random.Range(i + 1, _ids.Length);
      _ids[i] = _ids[_id];
      _ids[_id] = _intTmp;
    }
  }
}
