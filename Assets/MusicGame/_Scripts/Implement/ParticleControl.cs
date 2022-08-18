using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : IExecuteVector3Able
{
  private readonly List<GameObject> _particles;
  private GameObject _selected;

  public ParticleControl()
  {
    _particles = new List<GameObject>();
  }
  public void Execute(Vector3 param)
  {
    _selected = null;
    for (var i = 0; i < _particles.Count; i++)
    {
      if (_particles[i].activeSelf == false)
      {
        _selected = _particles[i];
        break;
      }
    }
    if (_selected == null)
    {
      _selected = MonoControl.Instant.SpawnParticle();
      _particles.Add(_selected);
    }
    _selected.transform.position = param;
    _selected.SetActive(true);
  }
}
