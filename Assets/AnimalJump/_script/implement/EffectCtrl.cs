using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : IExecuteTransformAble
{
  private readonly List<Deactivate> _effect;
  private readonly bool _isLanding;
  private int _id;
  public EffectCtrl(bool isLanding)
  {
    _effect = new List<Deactivate>();
    _isLanding = isLanding;
  }
  public void Execute(Transform param)
  {
    _id = -1;
    for (var i = 0; i < _effect.Count; i++)
    {
      if (_effect[i].gameObject.activeSelf == false)
      {
        _id = i;
        break;
      }
    }
    if (_id == -1)
    {
      _effect.Add(_isLanding ? Control.Instant.SpawnLadingEffect().GetComponent<Deactivate>() : Control.Instant.SpawnCollectEffect().GetComponent<Deactivate>());
      _id = _effect.Count - 1;
    }
    _effect[_id].transform.position = param.position;
    _effect[_id].target = param;
    _effect[_id].gameObject.SetActive(true);
  }
}
