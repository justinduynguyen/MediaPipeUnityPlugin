public static class Factory
{
  private static IExecuteVector3Able _BuffParticleControl;
  public static IExecuteAble CreateResolutionControl() { return new ResolutionCalculator(); }
  public static IExecuteAble CreateFollowerControl() { return new HandFollowControl(); }
  public static IExecuteFloatAble CreateMoleController() { return new MoleController(); }
  public static IExecuteBoolean CreatScoreDisplay() { return new ScoreUpdate(); }
  public static IExecuteVector3Able GetBuffGenerator()
  {
    if (_BuffParticleControl == null) { _BuffParticleControl = new BuffParticle(); }
    return _BuffParticleControl;
  }
}
