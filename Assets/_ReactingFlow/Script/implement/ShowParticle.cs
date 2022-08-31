using System.Collections.Generic;
using UnityEngine;

public class ShowParticle : IExecuteVector3Able
{
  private List<GameObject> _particles;
  private int _selected;
  private readonly ParticleType _type;
  public ShowParticle(ParticleType type)
  {
    _type = type;
    _particles = new List<GameObject>();
  }
  public void Execute(Vector3 param)
  {
    _selected = -1;
    for (var i = 0; i < _particles.Count; i++)
    {
      if (_particles[i].activeSelf == false)
      {
        _selected = i;
        break;
      }
    }
    if (_selected == -1)
    {
      switch (_type)
      {
        case ParticleType.heart: _particles.Add(Control.Instant.SpawnHeart()); break;
        case ParticleType.broken: _particles.Add(Control.Instant.SpawnBrokenHeart()); break;
        case ParticleType.star: _particles.Add(Control.Instant.SpawnStar()); break;
        default: Debug.LogError("Wrong ParticleType: " + _type); break;
      }
      _selected = _particles.Count - 1;
    }
    _particles[_selected].transform.position = param;
    _particles[_selected].SetActive(true);
  }
}
public enum ParticleType { heart, broken, star }
