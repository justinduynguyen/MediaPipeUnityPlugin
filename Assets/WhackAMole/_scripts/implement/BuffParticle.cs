using System.Collections.Generic;
using UnityEngine;

public class BuffParticle : IExecuteVector3Able
{
  private readonly List<GameObject> _particles;
  private GameObject _selected;
  private int _i;

  public BuffParticle()
  {
    _particles = new List<GameObject>();
  }
  public void Execute(Vector3 param)
  {
    _selected = null;
    for (_i = 0; _i < _particles.Count; _i++)
    {
      if (_particles[_i].activeSelf == false) { 
        _selected = _particles[_i];
        break;
      }
    }
    if (_selected == null)
    {
      _selected = WackAMoleControl.Instant.CreateBuffparticle();
      _particles.Add(_selected);
    }
    _selected.transform.position = param;
    _selected.SetActive(true);
  }
}
