// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using UnityEngine;

public class MoleController : IExecuteFloatAble
{  
  private readonly Mole[] _moles;
  private readonly IBooleanAble[] _stateMoles;
  private int _i;

  public MoleController()
  { 
    _moles = new Mole[WackAMoleControl.Instant.moles.Length];
    _stateMoles = new IBooleanAble[WackAMoleControl.Instant.moles.Length];
    for (_i = 0; _i < _moles.Length; _i++)
    {
      _moles[_i] = new Mole(2f, 4f, 3f, 6f);
      _stateMoles[_i] = WackAMoleControl.Instant.moles[_i].GetComponent<IBooleanAble>();
      if (_stateMoles[_i].GetValue() != _moles[_i].IsActive()) { _stateMoles[_i].Toggle(); }
    }
  }
  public void Execute(float param)
  {
    for (_i = 0; _i < _moles.Length; _i++)
    {
      if (_stateMoles[_i].GetValue() != _moles[_i].IsActive()) { _moles[_i].Cycle(_stateMoles[_i].GetValue() == false); }
      _moles[_i].TimePass(param);
      if (_stateMoles[_i].GetValue() != _moles[_i].IsActive()) { _stateMoles[_i].Toggle(); }
    }
  }
}

public class Mole
{
  private float _life, _dormant;
  private Vector2 _lifeMinMax, _dormainMinMax;
  public Mole(float lifeMin, float lifeMax, float dormantMin, float dormantMax)
  {
    _lifeMinMax.x = lifeMin;
    _lifeMinMax.y = lifeMax;
    _dormainMinMax.x = dormantMin;
    _dormainMinMax.y = dormantMax;
    Cycle(Random.Range(0f, 100f) < 50f);
  }
  public void Cycle(bool isDead)
  {
    if (isDead)
    {
      _life = 0;
      _dormant = Random.Range(_dormainMinMax.x, _dormainMinMax.y);
    }
    else { _life = Random.Range(_lifeMinMax.x, _lifeMinMax.y); }
  }
  public void TimePass(float time)
  {
    if(_life > 0f)
    {
      _life -= time;
      if (_life <= 0f) { Cycle(true); }
    }
    else
    {
      _dormant -= time;
      if (_dormant <= 0f) { Cycle(false); }
    }
  }
  public bool IsActive() { return _life > 0f; }
}
