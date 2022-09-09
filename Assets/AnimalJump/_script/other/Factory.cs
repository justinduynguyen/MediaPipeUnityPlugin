public static class Factory
{
  public static IExecuteAble CreateDimensionCtrl() { return new SceneSetting(); }
  public static IExecuteAble CreateFollower() { return new ShoulderTracker(); }
  public static IExecuteAble CreateFlow() { return new FlowCtrl(); }
  public static IExecuteAble CreateLogic() { return new GameLogic(); }
  public static IExecuteAble CreateFrog() { return new FrogMovement(); }
  public static IExecuteAble CreatePlatform() { return new GeneratePlatform(); }
  public static IExecuteAble CreateBgScroller() { return new BgScroll(); }
  public static IExecuteAble CreateProgress() { return new Progress(); }
  public static IExecuteIntAble CreateClock() { return new ShowTime(); }
  public static IExecuteIntAble CreateScoreDisplace() { return new ScoreDisplace(); }
  public static IBooleanAble CreateGroundCheck() { return new GroundChecker(); }
  public static IExecuteTransformAble CreateParticleCtrl(bool islanding) { return new EffectCtrl(islanding); }
}
