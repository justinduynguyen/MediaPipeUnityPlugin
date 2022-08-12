// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using UnityEngine;

public class SoccerLogic : IExecuteAble
{
  private float _playTime;
  private const float _SessionTime = 30f;
  private readonly IExecuteFloatAble _timeUI;

  public SoccerLogic()
  {
    _playTime = 0f;
    _timeUI = Factory.CreateUiSoccerTime();
  }
  public void Execute()
  {
    if (_playTime <= 0f) { ResetSession(); }
    else { RunGame(); }
    _playTime -= Time.deltaTime;
    if (_playTime <= 0f) { EndGame(); }
  }
  private void ResetSession() { _playTime = _SessionTime; }
  private void RunGame() { _timeUI.Execute(_playTime); }
  private void EndGame() { ControlMono.Instant.ChangeState(2); }
}
