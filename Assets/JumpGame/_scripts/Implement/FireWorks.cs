using System.Collections.Generic;
using UnityEngine;

public class FireWorks : IExecuteVector3Able
{
  private readonly List<GameObject> _fireWorks;
  private int _i, _selected;
  public FireWorks()
  {
    _fireWorks = new List<GameObject>();
  }
  public void Execute(Vector3 param)
  {
    _selected = -1;
    for (_i = 0; _i < _fireWorks.Count; _i++)
    {
      if (_fireWorks[_i].activeSelf == false)
      {
        _selected = _i;
        break;
      }
    }
    if (_selected < 0)
    {
      _fireWorks.Add(MonoControl.Instant.SpawnFireWork());
      _selected = _fireWorks.Count - 1;
    }
    _fireWorks[_selected].transform.position = param;
    _fireWorks[_selected].SetActive(true);
  }
}
