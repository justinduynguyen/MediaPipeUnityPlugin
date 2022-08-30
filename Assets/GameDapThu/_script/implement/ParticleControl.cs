using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : IParticleAble
{
  private readonly List<GameObject> _explosions, _monsterHits, _helmHits, _bombHit;
  private List<GameObject> _particles;
  private int _id;
  public ParticleControl()
  {
    _explosions = new List<GameObject>();
    _monsterHits = new List<GameObject>();
    _helmHits = new List<GameObject>();
    _bombHit = new List<GameObject>();
  }
  public void SpawnParticle(ParticleType type, Vector3 position)
  {
    switch (type)
    {
      case ParticleType.explosion: _particles = _explosions; break;
      case ParticleType.monster: _particles = _monsterHits; break;
      case ParticleType.helm: _particles = _helmHits; break;
      case ParticleType.bomb: _particles = _bombHit; break;
      default: Debug.Log("Wrong enum ParticleType: " + type); return;
    }
    _id = -1;
    for (var i = 0; i < _particles.Count; i++)
    {
      if (_particles[i].activeSelf == false)
      {
        _id = i;
        break;
      }
    }
    if (_id == -1)
    {
      _particles.Add(Control.Instant.SpawnParticle(type));
      _id = _particles.Count - 1;
    }
    _particles[_id].transform.position = position;
    _particles[_id].SetActive(true);
  }
}
