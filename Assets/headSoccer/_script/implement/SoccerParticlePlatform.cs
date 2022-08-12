using System.Collections.Generic;
using UnityEngine;

public class SoccerParticlePlatform : IExecuteVector3Able
{
  private GameObject _seleted;
  private readonly List<GameObject> _pool;
  private readonly int _id;
  private int i;
  public SoccerParticlePlatform(int id)
  { 
    _pool = new List<GameObject>();
    _id = id;
  }
  public void Execute(Vector3 param)
  {
    _seleted = null;
    for (i = 0; i < _pool.Count; i++)
    {
      if (_pool[i].activeSelf == false)
      {
        _seleted = _pool[i];
        break;
      }
    }
    if (_seleted == null)
    {
      _seleted = ControlMono.Instant.CreateParticle(_id);
      _pool.Add(_seleted);
    }
    _seleted.transform.position = param;
    _seleted.SetActive(true);
  }
}
